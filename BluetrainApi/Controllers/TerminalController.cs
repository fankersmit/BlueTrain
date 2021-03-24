using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BlueTrain.Terminal;
using BlueTrain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TerminalController : ControllerBase
    {
        private readonly ILogger<TerminalController> _logger;
        private readonly TerminalSettings _settings;
        private readonly ITerminal _terminal;

        public TerminalController(ILogger<TerminalController> logger, IOptions<TerminalSettings> terminalSettings)
        {
            _logger = logger;
            _settings = terminalSettings.Value;
            _terminal = new Terminal(_settings.Name, _settings.Description, _settings.Id);
        }

        [HttpGet]
        [Route("is-open")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string,string> IsOpen()
        {
            var key= "isopen:";
            string isOpen = _terminal.IsOpen().ToString().ToLower();
            return new Dictionary<string, string> {{key, isOpen}};
        }

        [HttpGet]
        [Route("is-closed")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string,string> IsClosed()
        {
            var key= "isclosed:";
            var value = _terminal.IsClosed().ToString().ToLower();
            return new Dictionary<string, string> {{key, value}};
        }

        [HttpGet]
        [Route("open")]
        [ProducesResponseType(typeof(TerminalStatusInformation), StatusCodes.Status200OK)]
        public TerminalStatusInformation Open()
        {
            _terminal.Open();
            return new TerminalStatusInformation
            {
                Status = Enum.GetName(_terminal.Status)
            };
        }

        [HttpGet]
        [Route("close")]
        [ProducesResponseType(typeof(TerminalStatusInformation), StatusCodes.Status200OK)]
        public TerminalStatusInformation Close()
        {
            _terminal.Close();
            return new TerminalStatusInformation
            {
                Status = Enum.GetName(_terminal.Status)
            };
        }

        [HttpGet]
        [Route("information")]
        [ProducesResponseType(typeof(TerminalInformation), StatusCodes.Status200OK)]
        public TerminalInformation GetInformation()
        {
            var info  = _terminal.GetTerminalInfo();
            return info;
        }

        [HttpGet]
        [Route("information/status")]
        [ProducesResponseType(typeof(TerminalStatusInformation), StatusCodes.Status200OK)]
        public TerminalStatusInformation GetStatus()
        {
            return new TerminalStatusInformation
            {
                Status = Enum.GetName(_terminal.Status)
            };
        }
    }
}
