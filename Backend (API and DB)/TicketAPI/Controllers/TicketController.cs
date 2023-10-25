using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketAPI.Exceptions;
using TicketAPI.Interfaces;
using TicketAPI.Models;
using TicketAPI.Models.DTOs;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private  readonly IAdminAction _adminAction;
        private readonly IInternAction _internAction;
        private readonly IFIlter _fIlter;
        private readonly ILogger<TicketController> _logger;

        public TicketController(IAdminAction adminAction, IInternAction internAction, IFIlter fIlter,ILogger<TicketController> logger)
        {
            _adminAction = adminAction;
            _internAction = internAction;
            _fIlter = fIlter;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Ticket),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RaiseTicket(Ticket ticket)
        {
            try
            {
                 var result = await _internAction.RaiseTicket(ticket);
                if (result == null)
                {
                    return BadRequest(new Error(400, "Can't able to raise ticket"));
                }
                return Ok(result);
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

        [HttpDelete]
        [ProducesResponseType(typeof(Ticket),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicket(Ticket ticket)
        {
            try
            {
                var result = await _internAction.DeleteTicket(ticket);
                if (result == null)
                {
                    return NotFound(new Error(400, "Ticket not found"));
                }
                return Ok(result);
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

        [HttpPut]
        [ProducesResponseType(typeof(Ticket),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditTicket(Ticket ticket)
        {
            try
            {
                var result = await _internAction.EditTicket(ticket);
                if (result == null)
                {
                    return NotFound(new Error(400, " Ticket not found"));
                }
                return Ok(result);
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

        [HttpPost]
        [ProducesResponseType(typeof(Solution),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSolution(Solution solution)
        {
            try
            {
                var result = await _adminAction.AnswerTicket(solution);
                if (result == null)
                {
                    return BadRequest(new Error(400, "Can't able to raise solution"));
                }
                return Ok(result);
            }
            catch (TicketNotFoundException tnfe)
            {
                _logger.LogError(tnfe.Message);
                return BadRequest(new Error(400, tnfe.Message));
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

        [HttpPut]
        [ProducesResponseType(typeof(Solution), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Solution>> EditSolution(Solution solution)
        {
            try
            {
                var result = await _adminAction.EditSolution(solution);
                if (result == null)
                {
                    return NotFound(new Error(400, "Solution not found"));
                }
                return Ok(result);
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

        [HttpDelete]
        [ProducesResponseType(typeof(Solution), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Solution>> DeleteSolution(Solution solution)
        {
            try
            {
                var result = await _adminAction.DeleteSolution(solution);
                if (result == null)
                {
                    return NotFound(new Error(400, "Solution not found"));
                }
                return Ok(result);
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

        [HttpGet]
        [ProducesResponseType(typeof(List<Ticket>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Ticket>>> GetAllUnAnsweredTicket()
        {
            try
            {
                var result = await _fIlter.GetAllUnAnsweredTicket();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new Error(400, "No tickets found"));
                }
                return Ok(result);
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



        [HttpPost]
        [ProducesResponseType(typeof(List<Ticket>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Ticket>>> GetAllUnAnsweredTicketBasedOnDateandUser(TicketFilterDTO ticketFilterDTO)
        {
            try
            {
                var result = await _fIlter.GetAllUnAnsweredTicketByDateandUser(ticketFilterDTO);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new Error(400, "No tickets found for the date"));
                }
                return Ok(result);
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



        [HttpGet]
        [ProducesResponseType(typeof(List<Ticket>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Ticket>>> GetAllTicketAndSolution()
        {
            try
            {
                var result = await _fIlter.GetAllTicketAndSolution();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new Error(400, "No Solution and ticket"));
                }
                return Ok(result);
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



        [HttpPost]
        [ProducesResponseType(typeof(List<Ticket>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Ticket>>> GetAllTicketAndSolutionBasedOnDateandUser(TicketFilterDTO ticketFilterDTO)
        {
            try
            {
                var result = await _fIlter.GetAllTicketAndSolutionByDateandUser(ticketFilterDTO);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new Error(400, "No Solution and ticket for the Date "));
                }
                return Ok(result);
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
