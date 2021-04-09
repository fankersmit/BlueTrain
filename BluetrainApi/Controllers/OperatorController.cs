using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using BlueTrain.Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BlueTrain.Terminal;
using Microsoft.AspNetCore.Http;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/terminal/[controller]")]
    [Produces("application/json")]
    public class OperatorController : ControllerBase
    {
        private readonly ILogger<TerminalController> _logger;
        private readonly TerminalSettings _settings;
        private readonly ITerminal _terminal;

        // ctor
        public OperatorController(
            ITerminal terminal,
            IHttpClientFactory httpClientFactory,
            ILogger<TerminalController> logger,
            IOptions<TerminalSettings> terminalSettings)
        {
            _logger = logger;
            _settings = terminalSettings.Value;

            // added as singleton (not really)
            _terminal = terminal;
        }

        // methods
        [HttpPost]
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

        [HttpPost]
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
    }
}
