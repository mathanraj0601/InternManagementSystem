using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTOs;

namespace UserAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly IUserAction _userAction;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserAction userAction,ILogger<UserController> logger)
        {
            _userAction = userAction;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserDTO>> Register(InternDTO internDTO)
        {
            try
            {
             var user = await _userAction.Register(internDTO);
                if (user == null)
                    return BadRequest(new Error(400, "Unable to register"));
                return Accepted();
            }
            catch(ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch(SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO)
        {
            try
            {
                var user = await _userAction.Login(userDTO);
                if (user == null)
                    return BadRequest(new Error(400, "Invalid UserId or Password"));
                return Ok(user);
            }
            catch (InternException ie)
            {
                return BadRequest(new Error(400, ie.Message));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(typeof(List<Intern>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Intern>>> GetApprovedInternBasedOnStatus(InternFilterDTO internFilterDTO)
        {
            try
            {
                var interns = await _userAction.GetApprovedInternBasedOnStatus(internFilterDTO);
                if (interns?.Count == 0)
                    return BadRequest(new Error(400, "No intern Found"));
                return Ok(interns);
            }
            catch(ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch(SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Intern>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Intern>>> GetAllIntern()
        {
            try
            {
                var interns = await _userAction.GetAllIntern();
                if (interns?.Count == 0)
                    return BadRequest(new Error(400, "No intern Found"));
                return Ok(interns);
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdatePassword(UserDTO user)
        {
            try
            {
                var intern = await _userAction.UpdatePassword(user);
                if (intern == null)
                    return BadRequest(new Error(400, "Unable to Update"));
                return Accepted();
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
               
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
            

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Intern>> ChangeInternStatus(User user)
        {
            try
            {
                var intern = await _userAction.ChangeInternStatus(user);
                if (intern == null)
                    return BadRequest(new Error(400, "Unable to Update"));
                return Accepted();
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server is not working properly"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }

        }

    }
}
