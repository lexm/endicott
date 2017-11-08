using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using endicott.Models;

namespace endicott.Controllers
{
    public class ProfileController : Controller
    {
        private EndicottContext _context;
        public ProfileController(EndicottContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("professional_profile")]
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                int id = (int)HttpContext.Session.GetInt32("userid");
                User profiled = _context.users.SingleOrDefault(login => login.userid == id);
                List<User> friends = _context.users
                                        .Include(u => u.Connectors)
                                        .Include(u => u.Connectees)
                                        .Where(u => (u.Connectors.Select(c => c.ConnectorId).Contains(id) || u.Connectees.Select(c => c.ConnectorId).Contains(id)))
                                        .Where(u => u.userid != id)
                                    .ToList();
                List<Connect> invites = _context.connections.Where(c => c.ConnecteeId == id && c.Accepted == false).Include(c => c.Connector).ToList();
                var profile = new ProfileViewModel{
                    user = profiled,
                    friends = friends,
                    invites = invites
                };
                return View(profile);
            }
            else
            {
                return RedirectToAction("LogReg", "User");
            }
        }

        [HttpGet]
        [Route("accept/{connid}")]
        public IActionResult Accept(int connid)
        {
            Connect invite = _context.connections.SingleOrDefault(conn => conn.connectid == connid);
            invite.Accepted = true;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        [Route("ignore/{connid}")]
        public IActionResult Ignore(int connid)
        {
            Connect invite = _context.connections.SingleOrDefault(conn => conn.connectid == connid);
            _context.connections.Remove(invite);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
    }
}