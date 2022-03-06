using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PersonItems.Dao;
using PersonItems.Models;

namespace PersonItems.Controllers
{
    public class ItemController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        public async Task<ActionResult> Index()
        {
            return View(await service.GetItemsAsync("SELECT * FROM Item"));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fullname,Bio,Age")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await service.AddItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<ActionResult> Edit(string id) => await ShowItem(id);

        private async Task<ActionResult> ShowItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = await service.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fullname,Bio,Age")] Item item)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<ActionResult> Delete(string id) => await ShowItem(id);

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id, Fullname, Bio, Age")] Item item)
        {
            await service.DeleteItemAsync(item);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id) => await ShowItem(id);

    }
}