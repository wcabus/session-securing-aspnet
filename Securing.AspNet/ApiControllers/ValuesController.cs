using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Securing.AspNet.ApiModels;
using Securing.AspNet.Filters;

namespace Securing.AspNet.ApiControllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        private static List<ValueModel> _values = new List<ValueModel>
        {
            new ValueModel { Id = Guid.NewGuid(), Value = "value1" },
            new ValueModel { Id = Guid.NewGuid(), Value = "value2" }
        };

        [Route]
        public IHttpActionResult Get()
        {
            return Ok(_values);
        }


        [Route("{id:guid}", Name = "GetValueById")]
        public IHttpActionResult GetById(Guid id)
        {
            var value = _values.SingleOrDefault(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        [Route, HttpPost]
        public IHttpActionResult Post([FromBody]PostValueModel model)
        {
            var newValue = new ValueModel { Id = Guid.NewGuid(), Value = model.Value };
            _values.Add(newValue);

            return CreatedAtRoute("GetValueById", new { newValue.Id }, newValue);
        }

        [Route("~/api/csrf/values", Name = "PostValuesWithCsrf"), HttpPost]
        [ValidateApiAntiForgeryToken]
        public IHttpActionResult PostCsrf([FromBody]PostValueModel model)
        {
            var newValue = new ValueModel { Id = Guid.NewGuid(), Value = model.Value };
            _values.Add(newValue);

            return CreatedAtRoute("GetValueById", new { newValue.Id }, newValue);
        }
    }
}
