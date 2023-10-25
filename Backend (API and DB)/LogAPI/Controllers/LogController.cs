using LogAPI.Exceptions;
using LogAPI.Interfaces;
using LogAPI.Models;
using LogAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    [EnableCors("MyCors")]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly ILogAction _action;

        public LogController(ILogAction action,ILogger<LogController> logger)
        {
            _logger = logger;
            _action = action;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Log),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Log>> AddLog([FromBody] Log log)
        {
            try
            {
                var result = await _action.AddLog(log);
                if (result != null)
                {
                    return Created("home", result);
                }
                return BadRequest(new Error(400, "No log found"));
            }
            catch(LogException le)
            {
                _logger.LogError(le.Message);
                return BadRequest(new Error(400, le.Message));
            }
            catch(ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server not working"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize(Roles = "Admin,intern")]
        [HttpPut]
        [ProducesResponseType(typeof(Log), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Log>> EditLog([FromBody] Log log)
        {
            try
            {
                var result = await _action.EditLog(log);
                if (result != null)
                {
                    return Accepted();
                }
                return NotFound(new Error(400, "No log found"));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));

            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server not working"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        [ProducesResponseType(typeof(Log), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Log>> GetLogBasedOnUserandDate(LogFilterDTO logFilterDTO)
        {
            try
            {
                var result = await _action.GetAllLogsBasedOnUser(logFilterDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(400, "No log found"));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));

            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server not working"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize(Roles = "admin,intern")]

        [HttpPost]
        [ProducesResponseType(typeof(List<Log>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Log>>> GetAllLogsBasedonUserAndDate(LogFilterDTO logFilterDTO)
        {
            try
            {
                var result = await _action.GetAllLogsBasedonUserAndDate(logFilterDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(400, "No log found"));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server not working"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }

        [Authorize(Roles ="intern")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Log>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Log>>> GetAllLog()
        {
            try
            {
                var result = await _action.GetAllLog();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(new Error(400, "No log found"));
            }
            catch (ContextException ce)
            {
                _logger.LogError(ce.Message);
                return BadRequest(new Error(400, ce.Message));
            }
            catch (SqlException se)
            {
                _logger.LogError(se.Message);
                return BadRequest(new Error(400, "Server not working"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(400, "Something went wrong"));
            }
        }


    }
}
