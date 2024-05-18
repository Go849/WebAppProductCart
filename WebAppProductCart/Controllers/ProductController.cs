//using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebAppProductCart.Models;

namespace WebAppProductCart.Controllers
{
    public class ProductController : Controller
    {
        ProjectContext _db;
        public ProductController(ProjectContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(_db.tblProduct.ToList());
        }
        public IActionResult AddCart(int id) 
        {
            List<Product> oldcart = null;
            var oldcart_string = HttpContext.Session.GetString("cart");//fetching the old cart if exist
            
            if (oldcart_string != null)
            {
                oldcart = JsonConvert.DeserializeObject<List<Product>>(oldcart_string);//populate the oldcart List
            }
            if (oldcart==null)//if oldcart is empty
            {
                List<Product> productCart = new List<Product>();
                productCart.Add(_db.tblProduct.Where(a => a.ProductId == id).FirstOrDefault());//fetcing the product that is currently added to cart
                string string_productCart=JsonConvert.SerializeObject(productCart);//convert the cart list to string
                HttpContext.Session.SetString("cart", string_productCart);//save in session
            }
            else
            {
                oldcart.Add(_db.tblProduct.Where(a => a.ProductId == id).FirstOrDefault());//fetcing the product that is currently added to cart
                string string_productCart = JsonConvert.SerializeObject(oldcart);//convert the cart list to string
                HttpContext.Session.SetString("cart", string_productCart);//save in session
            }
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            List<Product> oldcart = null;
            var oldcart_string = HttpContext.Session.GetString("cart");//fetching the old cart if exist

            if (oldcart_string != null)
            {
                oldcart = JsonConvert.DeserializeObject<List<Product>>(oldcart_string);//populate the oldcart List
            }

            return View(oldcart);
        }

        public ActionResult Delete_cart(int id)
        {
            List<Product> oldcart = null;
            var oldcart_string = HttpContext.Session.GetString("cart");//fetching the old cart if exist

            if (oldcart_string != null)
            {
                oldcart = JsonConvert.DeserializeObject<List<Product>>(oldcart_string);//populate the oldcart List
            }

            foreach(var item in oldcart)
            {
                if(item.ProductId == id) 
                {
                    oldcart.Remove(item);
                    break;
                }
            }
            string string_productCart = JsonConvert.SerializeObject(oldcart);//convert the cart list to string
            HttpContext.Session.SetString("cart", string_productCart);//save in session
            return RedirectToAction("Cart");
        }

    }
}
