using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TheCodeCamp.Data;
using TheCodeCamp.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace TheCodeCamp.Controllers
{
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();

                //Mapping
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                //TODO Add logging
                return InternalServerError(ex);
            }
            
        }
        [Route("{moniker}")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);
                return Ok(_mapper.Map<CampModel>(result));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }

}