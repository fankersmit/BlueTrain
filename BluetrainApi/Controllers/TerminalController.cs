﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using BlueTrain.Containers;
using BlueTrain.Terminal;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class TerminalController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TerminalController> _logger;
        private readonly TerminalSettings _settings;
        private readonly ITerminal _terminal;

        // ctor
        public TerminalController(
            ITerminal terminal,
            IHttpClientFactory httpClientFactory,
            ILogger<TerminalController> logger,
            IOptions<TerminalSettings> terminalSettings)
        {
            _logger = logger;
            _settings = terminalSettings.Value;

            // added as singleton (not really)
            _terminal = terminal;

            // Create httpClients for sending containers
            _httpClientFactory = httpClientFactory;
        }

        // methods


        [HttpGet]
        [Route("holdingyard/information")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public IHoldingYardInformation GetHoldingYardInformation()
        {
            return _terminal.HoldingYard.GetHoldingYardInfo();
        }

        [HttpGet]
        [Route("holdingyard/is-empty")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string, string> HoldingYardIsEmpty()
        {
            var key = "isempty";
            var isempty = _terminal.HoldingYard.IsEmpty.ToString().ToLower();
            return new Dictionary<string, string> {{key, isempty}};
        }

        [HttpGet]
        [Route("holdingyard/is-filled")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string, string>  HoldingYardIsFilled()
        {
            var key = "isfilled";
            var isfilled = _terminal.HoldingYard.IsFilled.ToString().ToLower();
            return new Dictionary<string, string> {{key, isfilled }};
        }

        [HttpGet]
        [Route("holdingyard/capacity")]
        [ProducesResponseType(typeof(Dictionary<string,int>), StatusCodes.Status200OK)]
        public Dictionary<string, int> HoldingYardCapacity()
        {
            var key = "capacity";
            var capacity = _terminal.HoldingYard.Capacity;
            return new Dictionary<string, int> {{ key, capacity }};
        }

        [HttpGet]
        [Route("holdingyard/containers/count")]
        [ProducesResponseType(typeof(Dictionary<string,int>), StatusCodes.Status200OK)]
        public Dictionary<string, int> ContainersCount()
        {

            var key = "count";
            var count = _terminal.HoldingYard.Count;
            return new Dictionary<string, int> {{ key, count }};
        }

        [HttpPost]
        [Route("receive")]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Receive([FromBody] object container  )
        {
            var ctr = JsonSerializer.Deserialize<Container>(container.ToString());
            Guid containerID = ctr.Id;

            // check if terminal is open  -> 403
            if (_terminal.IsClosed())
            {
                //return Forbid(); // 403, turned into 404 by ASP.NET Core's authentication logic
                return StatusCode(403);
            }

            // check if container is already in yard -> 403
            if( _terminal.HoldingYard.Find(containerID) != null )
            {
                //return Forbid();
                return StatusCode(403);
            }

            // put container in yard -> created 201
            _terminal.HoldingYard.Add(ctr);
            return Created($"api/v1/terminal/holdingyard/{containerID}", ctr); // Created();
        }

        [HttpGet]
        [Route("is-open")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string,string> IsOpen()
        {
            var key= "isopen";
            string isOpen = _terminal.IsOpen().ToString().ToLower();
            return new Dictionary<string, string> {{key, isOpen}};
        }

        [HttpGet]
        [Route("is-closed")]
        [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status200OK)]
        public Dictionary<string,string> IsClosed()
        {
            var key= "isclosed";
            var value = _terminal.IsClosed().ToString().ToLower();
            return new Dictionary<string, string> {{key, value}};
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
