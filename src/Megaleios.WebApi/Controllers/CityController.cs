using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Megaleios.Data.Entities;
using Megaleios.Domain;
using Megaleios.Domain.ViewModels;
using Megaleios.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using RestSharp;
using UtilityFramework.Application.Core;
using UtilityFramework.Application.Core.ViewModels;

namespace Megaleios.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/v1/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CityController(ICityRepository cityRepository, IStateRepository stateRepository, IMapper mapper, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }




        /// <summary>
        /// LISTAR CIDADES POR ESTADOS
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{stateId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] string stateId)
        {
            try
            {
                var listCity = await _cityRepository.FindByAsync(x => x.StateId.Equals(stateId), Builders<City>.Sort.Ascending(nameof(City.Name))).ConfigureAwait(false);

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<CityDefaultViewModel>>(listCity)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro(responseList: true));
            }
        }

        /// <summary>
        /// LISTAR TODOS PAISES
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("ListCountry")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(typeof(CountrySelectViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListCountry()
        {
            try
            {
                var listCountry = await _countryRepository.FindAllAsync(Builders<Country>.Sort.Ascending(nameof(Country.Name)));

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<CountrySelectViewModel>>(listCountry)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro(responseList: true));
            }
        }

        /// <summary>
        /// LISTAR ESTADOS COM FILTRO POR ESTADO
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("ListState")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListState([FromQuery] string countryId)
        {
            var listState = new List<State>();
            try
            {

                if (string.IsNullOrEmpty(countryId) == false)
                {
                    listState = await _stateRepository.FindByAsync(x => x.CountryId == countryId, Builders<State>.Sort.Ascending(nameof(State.Name))).ConfigureAwait(false) as List<State>;
                }
                else
                {
                    listState = await _stateRepository.FindAllAsync(Builders<State>.Sort.Ascending(nameof(State.Name))).ConfigureAwait(false) as List<State>;
                }

                return Ok(Utilities.ReturnSuccess(data: _mapper.Map<IEnumerable<StateDefaultViewModel>>(listState)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }

        /// <summary>
        /// BUSCAR INFORMAÇÕES DE DETERMINADO CEP
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetInfoFromZipCode/{zipCode}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReturnViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetInfoFromZipCode([FromRoute] string zipCode)
        {
            try
            {
                if (string.IsNullOrEmpty(zipCode))
                    return BadRequest(Utilities.ReturnErro(DefaultMessages.RequiredZipCode));


                /*API EM NODE CONTIDA NA PASTA node_service*/
                var client = new RestClient($"https://api-readboleto.megaleios.com/seachZipCode/{zipCode.OnlyNumbers()}");

                var request = new RestRequest(Method.GET);

                var infoZipCode = await client.Execute<AddressInfoViewModel>(request).ConfigureAwait(false);


                if (infoZipCode.StatusCode != HttpStatusCode.OK || infoZipCode.Data == null)
                    return BadRequest(Utilities.ReturnErro(DefaultMessages.ZipCodeNotFoundOrOffline));

                if (infoZipCode.Data.Erro)
                    return BadRequest(Utilities.ReturnErro(DefaultMessages.ZipCodeNotFound));

                var response = _mapper.Map<InfoAddressViewModel>(infoZipCode.Data);

                var builder = Builders<City>.Filter;
                var conditions = new List<FilterDefinition<City>>();

                conditions.Add(builder.Regex(x => x.Name, new BsonRegularExpression(new Regex($"^{infoZipCode.Data.Localidade}$", RegexOptions.IgnoreCase))));
                conditions.Add(builder.Where(x => x.StateUf == infoZipCode.Data.Uf));

                var city = await _cityRepository.GetCollectionAsync().FindSync(builder.And(conditions)).FirstOrDefaultAsync().ConfigureAwait(false);

                if (city == null)
                    return Ok(Utilities.ReturnSuccess(data: response));

                response.ZipCode = Convert.ToUInt64(zipCode.OnlyNumbers()).ToString(@"00000\-000");
                response.CityId = city._id.ToString();
                response.CityName = city.Name;
                response.StateId = city.StateId;
                response.StateUf = infoZipCode.Data.Uf;
                response.StateName = city.StateName;
                response.Ibge = infoZipCode.Data.Ibge;
                response.Gia = infoZipCode.Data.Gia;

                return Ok(Utilities.ReturnSuccess(data: response));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }

    }
}