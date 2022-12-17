using ContractorsHub.Core.Models.Admin;
using ContractorsHub.Hubs;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ContractorsHub.Areas.Admin.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IRepository repo;

        public ChatController(IRepository _repo)
        {
            repo = _repo;
        }

        public  async Task<IActionResult> LiveChat(string id)
        {
            var model = await repo.AllReadonly<Message>()
                .Where(x => x.UserId == id)
                .Select(x => new MessageViewModel()
                {
                    UserId = x.UserId,
                    Id = x.Id,
                    ConnectionId = x.ConnectionId,
                    CreatedOn = x.CreatedOn
                   
                }).FirstAsync();
            return View(model);
        }

        public async Task<IActionResult> MessageRequest()
        {
            var model = await repo.AllReadonly<Message>()
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new MessageViewModel()
                {
                    Id = x.Id,
                    ConnectionId = x.ConnectionId,
                    UserId = x.UserId,
                    CreatedOn = x.CreatedOn
                }).ToListAsync();
            //hubContext
            return View(model);
        }
    }
}
