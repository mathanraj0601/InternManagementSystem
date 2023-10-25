using System.Security.Cryptography;
using System.Text;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTOs;

namespace UserAPI.Services
{
    public class UserService : IUserAction
    {
        private readonly IRepo<Intern, int> _internRepo;
        private readonly IRepo<User, int> _userRepo;
        private readonly ITokenGenerate _tokenGenerate;
        private readonly IGeneratePassword _generatePassword;

        public UserService(IRepo<Intern,int> internRepo, IRepo<User,int> userRepo,ITokenGenerate tokenGenerate, IGeneratePassword generatePassword)
        {
            _internRepo = internRepo;
            _userRepo = userRepo;
            _tokenGenerate = tokenGenerate;
            _generatePassword = generatePassword;
        }

        public async Task<Intern?> ChangeInternStatus(User user)
        {
            Intern? existingUser = await _internRepo.Get(user.Id);
            if(existingUser != null && existingUser.User!=null)
            {
                existingUser.User.Status = user.Status;
                var updatedUser = await _userRepo.Update(existingUser.User);
                if(updatedUser != null)
                    return await _internRepo.Get(updatedUser.Id);

            }
            return null;
        }

        public async Task<List<Intern>?> GetAllIntern()
        {
            var interns = await _internRepo.GetAll();
            if(interns != null)
            {
                foreach (var intern in interns)
                {
                    if (intern.User != null)
                    {
                        intern.User.PasswordKey = null;
                        intern.User.PasswordHash = null;
                    }


                }
                return interns.ToList();
            }

            return null; ;
        }

        public async Task<List<Intern>?> GetApprovedInternBasedOnStatus(InternFilterDTO internFilterDTO)
        {
            var interns=  (await _internRepo.GetAll())?.Where(i=>i.User?.Status == internFilterDTO.Status).ToList();
            foreach(var intern in interns ?? new List<Intern>())
            {
                if(intern.User!=null)
                {
                    intern.User.PasswordKey = null;
                    intern.User.PasswordHash = null;
                }
               

            }
            return interns;
            
        }

        public async Task<UserDTO?> Login(UserDTO userDTO)
        {
            
            User? user = await _userRepo.Get(userDTO.UserID);
            if(user != null && !user.Status)
            {
                throw new InternException("Not approved yet");
            }
            if(user != null && userDTO.Password!=null && user.PasswordKey!= null && user.PasswordHash!= null)
            {
                var hmac = new HMACSHA256(user.PasswordKey);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (user.PasswordHash!=null && computedHash[i] != user.PasswordHash[i])
                        return null;
                }
                UserDTO returnUser = new UserDTO();
                returnUser.UserID = user.Id;
                returnUser.Role = user.Role;
                returnUser.Token = await _tokenGenerate.GenerateJSONWebToken(new UserDTO { UserID = user.Id, Role= user.Role }) ;
                return returnUser;

            }
            return null;

        }

        public async Task<UserDTO?> Register(InternDTO internDTO)
        {
            if(internDTO.User != null)
            {
                var hmac = new HMACSHA256();
                internDTO.PasswordClear = _generatePassword.GeneratePassword(internDTO);
                internDTO.User.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(internDTO.PasswordClear ?? "1234"));
                internDTO.User.PasswordKey = hmac.Key;
                internDTO.User.Role = internDTO.User.Role?.ToLower();
                var intern = await _internRepo.Add(internDTO);
                if(intern != null && intern.User!= null)
                {
                    UserDTO returnUser = new UserDTO();
                    returnUser.UserID = intern.User.Id;
                    returnUser.Role = intern.User.Role;
                    returnUser.Token = await _tokenGenerate.GenerateJSONWebToken(returnUser);
                    return returnUser;
                }
                
            }
            return null;
            
        }

        public async Task<User?> UpdatePassword(UserDTO userDTO)
        {
            User? user = await _userRepo.Get(userDTO.UserID);
            if(user != null && userDTO.NewPassword != null)
            {
                var hmac = new HMACSHA256();
                user.PasswordKey = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.NewPassword));
                var existingUser = await _userRepo.Update(user);
                if(existingUser != null)
                {
                    User returnUser = new User();
                    returnUser.Id = existingUser.Id;
                    returnUser.Role = existingUser.Role;
                    returnUser.Status = existingUser.Status;
                    return returnUser;
                }
            }
            return null;
        }


    }
}
