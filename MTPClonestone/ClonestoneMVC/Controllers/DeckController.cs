using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ClonestoneMVC.Controllers
{
    public class DeckController : Controller
    {
               
        [HttpGet]
        public string GetDeck()
        {
            return null;
        }

        // GET: Deck
        [HttpPost]
        public string GetDeck(int idDeck = 1)
        {
            using (ClonestoneEntities cont = new ClonestoneEntities())
            {               
                var deck = cont.pGetDeckTextOnly(idDeck);

                string deckJson;
                deckJson = JsonConvert.SerializeObject(deck);
                return deckJson;

            }
        }
    }
}