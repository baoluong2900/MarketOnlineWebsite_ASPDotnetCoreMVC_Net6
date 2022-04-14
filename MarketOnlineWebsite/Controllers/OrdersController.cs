using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Helpper;
using MarketOnlineWebsite.Models;
using MarketOnlineWebsite.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnlineWebsite.Controllers
{
    public class OrdersController : Controller
    {
        private readonly dbMarketsContext _context;

        /// <summary> Đơn hàng ở trạng thái xác nhận</summary> 
        private const int transactStatusID_14 = 14;

        public INotyfService _INotyfService { get; }
        public OrdersController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }


        public async Task<IActionResult> Delete(int? id)
        {
            Order order = _context.Orders.SingleOrDefault(x => x.OrderId == id);
            try
            {
                order.Deleted = true;
                _context.Update(order);

                _INotyfService.Success("Trả hàng thành công");
                await _context.SaveChangesAsync();
                //return NotFound();
                return RedirectToAction("Login", "Accounts");
            }
            catch (DbUpdateConcurrencyException)
            {
                _INotyfService.Error("Cập nhật thất bại");
                return NotFound();

            }
            return RedirectToAction("Login", "Accounts");
            //return Redirect("my-account.html");
        }

        //GET: Orders/Details/
        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var accountID = HttpContext.Session.GetString("CustomerId");
                if (string.IsNullOrEmpty(accountID))
                {
                    return RedirectToAction("Login", "Accounts");
                }
                var customer = _context.Customers.AsNoTracking()
                    .SingleOrDefault(x => x.CustomerId == int.Parse(accountID));
                if (customer == null)
                {
                    return NotFound(); 
                }

                var order =_context.Orders
                    .Include(x => x.TransactStatus)
                    .FirstOrDefault(x => x.OrderId == id && int.Parse(accountID) == x.CustomerId);

                if (order == null)
                {
                    return NotFound();
                }
                var orderDetails = _context.OrderDetails
                    .Include(x => x.Product)
                    .AsNoTracking()
                    .Where(x => x.OrderId == id)
                    .OrderBy(x => x.OrderDetailId)
                    .ToList();
                
                ViewOrderVM orderVM = new ViewOrderVM();
                orderVM.Order = order;
                orderVM.OrderDetail = orderDetails;
                orderVM.TransactStatus = GetTransactStatus(order.TransactStatusId);
                orderVM.FullName = customer.FullName;
                orderVM.Phone = customer.Phone;
                orderVM.Payment = Utilities.GetPaymentName(order.PaymentId.Value);
                orderVM.Phone=customer.Phone;
                orderVM.Address = order.Address;
                orderVM.PhuongXa = GetNameLocation(order.Ward.Value);
                orderVM.QuanHuyen = GetNameLocation(order.District.Value);
                orderVM.TinhThanh = GetNameLocation(order.LocationId.Value);
                orderVM.CheckOrder = transactStatusID_14;
                //orderVM.Location = location;
                return PartialView("Details", orderVM);

            }
            catch {
                return NotFound();
            }
        }
        public string GetNameLocation(int idLocation)
        {
            try
            {
                string nameFetchedId = _context.Locations.SingleOrDefault(x => x.LocationId == idLocation)?.Name;
                return nameFetchedId;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public string GetTransactStatus(int idTransactStatus)
        {
            try
            {
                string nameFetchedId = _context.TransactStatuses.SingleOrDefault(x => x.TransactStatusId == idTransactStatus)?.Status;
                return nameFetchedId;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
