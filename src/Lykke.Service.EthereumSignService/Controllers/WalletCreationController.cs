﻿using System.Net;
using System.Threading.Tasks;
using Lykke.Service.EthereumSignService.Core.Services;
using Lykke.Service.EthereumSignService.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.EthereumSignService.Controllers
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    [Route("api/wallets")]
    public class WalletsController : Controller
    {
        private readonly IWalletCreationService _walletCreationService;

        public WalletsController(IWalletCreationService walletCreationService)
        {
            _walletCreationService = walletCreationService;
        }

        /// <summary>
        /// Checks service is alive
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreateWallet")]
        [ProducesResponseType(typeof(KeyModelResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWallet()
        {
            var keyModel = await _walletCreationService.CreateKeyModelAsync();

            return Ok(new KeyModelResponse()
            {
                PrivateKey = keyModel.PrivateKey,
                PublicAddress = keyModel.PublicAddress
            });
        }
    }
}
