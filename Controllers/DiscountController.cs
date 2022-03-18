using System.ComponentModel;
using System.Reflection.Emit;
using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using DiscountCodes.DTOs;
using DiscountCodes.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DiscountCodes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountDB _discountDB;

        public DiscountController(DiscountDB discountDB)
        {
            _discountDB = discountDB;
        }

        /// <summary>
        /// Retrieves a discount code for the given store and returns it. The discount code is claimed by the user and can't be used any more.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns>The discount code object</returns>
        [HttpGet("{storeId:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DiscountCode> GetByStoreId(Guid storeId)
        {
            var userId = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var code = _discountDB.DiscountCodes.FirstOrDefault(p => p.StoreId == storeId && p.ClaimedDate == null);
            if (code == null) {
                return NotFound();    //Fail early on error
            }

            code.ClaimedByUserId = userId;
            code.ClaimedDate = DateTime.Now;

            _discountDB.SaveChanges();

            return code;
        }

        /// <summary>
        /// Creates a series of discount codes based on store and number of wanted codes.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>The created discount code</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DiscountCodeDTO> Create(DiscountCodeCreateDTO code)
        {
            var store = _discountDB.Stores.FirstOrDefault(p => p.Id == code.StoreId);
            if (store == null) {
                return NotFound("StoreID is invalid.");    //Fail early on error
            }
            List<DiscountCode> newcodes = new List<DiscountCode>();

            // Tn a real product I would have extracted this piece into a sepearate worker to make the generation of the codes totally asynchronous
            for (int i = 0; i < code.NumberOfCodes; i++)
            {
                var discountCode = new DiscountCode() {
                    Id = Guid.NewGuid(),
                    Code = $"{store.ShortName}-{i}", //Extremely simple generation of the discount codes...
                    Created = DateTime.Now,
                    StoreId = code.StoreId
                };
                newcodes.Add(discountCode);
            }

            _discountDB.DiscountCodes.AddRange(newcodes);
            _discountDB.SaveChanges();

            return Created("/", code);
        }
    }
}
