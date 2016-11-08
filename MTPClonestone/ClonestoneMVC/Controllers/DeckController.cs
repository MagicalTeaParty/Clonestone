using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClonestoneMVC.Models;

namespace ClonestoneMVC.Controllers
{
    public class DeckController : Controller
    {
               
        [HttpGet]
        public List<tblcard> GetDeck()
        {
            return null;
        }

        // GET: Deck
        [HttpPost]
        public List<tblcard> GetDeck(int idDeck = 1)
        {
            using (ClonestoneEntities cont = new ClonestoneEntities())
            {
                var deck = (from c in cont.tblcards
                            join dc in cont.tbldeckcards
                            on c.idcard equals dc.fkcard
                            where dc.fkdeck == idDeck
                            select c).ToList();        


                

            }

            return null;
        }
    }
}