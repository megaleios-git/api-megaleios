using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UtilityFramework.Application.Core;
using UtilityFramework.Application.Core.ViewModels;
using Megaleios.Data.Entities;
using Megaleios.Domain.ViewModels;
using Megaleios.Repository.Interface;
using System.Linq;

namespace Megaleios.WebApi.Controllers
{

    [EnableCors("AllowAllOrigin")]
    [Route("api/v1/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepository;
        private readonly IBankBrazilRepository _bankBrazilRepository;
        private readonly IMapper _mapper;

        public BankController(IBankRepository bankRepository, IMapper mapper, IBankBrazilRepository bankBrazilRepository)
        {
            _bankRepository = bankRepository;
            _mapper = mapper;
            _bankBrazilRepository = bankBrazilRepository;
        }


        /// <summary>
        /// LISTA DE BANCOS DISPONIVEIS PARA CADASTRO (IUGU)
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet("List")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listBank = await _bankRepository.FindAllAsync(Builders<Bank>.Sort.Ascending(nameof(Bank.Name))).ConfigureAwait(false);

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<BankViewModel>>(listBank)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }

        /// <summary>
        /// LISTA DE BANCOS DO BRASIL
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet("ListBrazilian")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListBrazilian()
        {
            try
            {
                var listBank = await _bankBrazilRepository.FindAllAsync(Builders<BankBrazil>.Sort.Ascending(nameof(BankBrazil.Name))).ConfigureAwait(false);

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<BankViewModel>>(listBank)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }

        /// <summary>
        /// ATUALIZAR BASE DE DADOS
        /// </summary>
        /// <remarks>
        /// OBJ DE ENVIO
        /// 
        /// 
        ///         POST
        ///             {
        ///              "Banco":"string",
        ///              "Agencia":"string",
        ///              "Conta":"string",
        ///              "Code":"string",
        ///             }
        /// </remarks>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromQuery] bool noUpdate, [FromBody] List<BankImportViewModel> model)
        {
            try
            {
                var listBank = await _bankRepository.FindAllAsync() as List<Bank>;

                model = model.OrderBy(x => x.Name).DistinctBy(x => x.Name).ToList();

                for (int i = 0; i < model.Count; i++)
                {
                    var item = model[i];

                    var bankItem = listBank.Find(x => x.Code == item.Code);

                    if (bankItem != null)
                    {
                        if (noUpdate == false)
                        {
                            bankItem.Name = item.Name;
                            bankItem.AgencyMask = item.AgencyMask;
                            bankItem.AccountMask = item.AccountMask;

                            await _bankRepository.UpdateAsync(bankItem);
                        }
                    }
                    else
                    {

                        if (await _bankRepository.CountAsync(x => x.Name == item.Name) > 0)
                            continue;

                        bankItem = new Bank()
                        {
                            Code = item.Code,
                            Name = item.Name,
                            AgencyMask = item.AgencyMask,
                            AccountMask = item.AccountMask,
                        };

                        await _bankRepository.CreateAsync(bankItem);
                    }
                }

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<BankViewModel>>(listBank)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }
    }
}