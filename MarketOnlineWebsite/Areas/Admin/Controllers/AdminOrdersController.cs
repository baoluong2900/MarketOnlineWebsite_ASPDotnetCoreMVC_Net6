#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketOnlineWebsite.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList.Core;

namespace MarketOnlineWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminOrdersController : Controller
	{
		private readonly dbMarketsContext _context;
		public INotyfService _INotyfService { get; }

		public AdminOrdersController(dbMarketsContext context, INotyfService inotyfService)
		{
			_context = context;
			_INotyfService = inotyfService;
		}

		// GET: Admin/AdminOrders
		public async Task<IActionResult> Index(int? page)
		{
			// xử lý phân trang
			var pageNumber = page == null || page < 0 ? 1 : page.Value;
			var pageSize = 20;
			var lsOrders = _context.Orders.AsNoTracking().Include(o => o.Customer).Include(o => o.TransactStatus).Where(m => m.Deleted != true);
			PagedList<Order> models = new PagedList<Order>(lsOrders, pageNumber, pageSize);
			ViewBag.CurrentPage = pageNumber;

			return View(models);
		}

		// GET: Admin/AdminOrders/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.Customer)
				.Include(o => o.TransactStatus)
				.FirstOrDefaultAsync(m => m.OrderId == id);
			if (order == null)
			{
				return NotFound();
			}
			var orderDetail = _context.OrderDetails.AsNoTracking()
				.Include(x => x.Product)
				.Where(x => x.OrderId == order.OrderId)
				.OrderBy(x => x.OrderDetailId)
				.ToList();
			ViewBag.Detail = orderDetail;
			string TinhThanh = GetNameLocation(order.LocationId.Value);
			string QuanHuyen = GetNameLocation(order.District.Value);
			string PhuongXa = GetNameLocation(order.Ward.Value);
			string addressLoaction = $"{PhuongXa}, {QuanHuyen}, {TinhThanh}";
			ViewBag.AddressLoaction = addressLoaction;

			return View(order);
		}

		// GET: Admin/AdminOrders/Create
		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
			ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId");
			return View();
		}

		// POST: Admin/AdminOrders/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,TotalMoney,PaymentId,Note,Address,LocationId,District,Ward")] Order order)
		{
			if (ModelState.IsValid)
			{
				_context.Add(order);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
			ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
			return View(order);
		}

		// GET: Admin/AdminOrders/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}
			ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
			ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
			return View(order);
		}

		// POST: Admin/AdminOrders/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,TotalMoney,PaymentId,Note,Address,LocationId,District,Ward")] Order order)
		{
			if (id != order.OrderId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(order);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!OrderExists(order.OrderId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
			ViewData["TransactStatusId"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "TransactStatusId", order.TransactStatusId);
			return View(order);
		}

		// GET: Admin/AdminOrders/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.Customer)
				.Include(o => o.TransactStatus)
				.FirstOrDefaultAsync(m => m.OrderId == id);
			string TinhThanh = GetNameLocation(order.LocationId.Value);
			string QuanHuyen = GetNameLocation(order.District.Value);
			string PhuongXa = GetNameLocation(order.Ward.Value);
			string addressLoaction = $"{PhuongXa}, {QuanHuyen}, {TinhThanh}";
			ViewBag.AddressLoaction = addressLoaction;
			if (order == null)
			{
				return NotFound();
			}
			return View(order);
		}

		// POST: Admin/AdminOrders/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var order = await _context.Orders.FindAsync(id);
			order.Deleted = true;
			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> ChangeStatus(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var order = await _context.Orders.AsNoTracking().Include(x => x.Customer).Include(x => x.TransactStatus).FirstOrDefaultAsync(x => x.OrderId == id);
			if (order == null)
			{
				return NotFound();
			}
			ViewData["LsTransactStatus"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "Status", order.TransactStatusId);
			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeStatus(int id, [Bind("OrderId,CustomerId,OrderDate,ShipDate,TransactStatusId,Deleted,Paid,PaymentDate,TotalMoney,PaymentId,Note,Address,LocationId,District,Ward")] Order order)
		{
			if (id != order.OrderId)
			{
				return NotFound();
			}
			try
			{
				var orders = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == id);
				if (orders != null)
				{
					orders.Paid = order.Paid;
					if (order.Deleted == true)
					{
						orders.TransactStatusId = 1002;
					}
					else
					{
						orders.TransactStatusId = order.TransactStatusId;
					}

					if (orders.Paid == true)
					{
						orders.PaymentDate = DateTime.Now;
					}
					if (orders.TransactStatusId == 17)
					{
						order.Deleted = true;
					}
					if (orders.TransactStatusId == 15)
					{
						orders.ShipDate = DateTime.Now;
					}
				}
				_context.Update(orders);
				await _context.SaveChangesAsync();
				_INotyfService.Success("Cập nhật trạng thái đơn hàng thành công");
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(order.OrderId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));

			ViewData["LsTransactStatus"] = new SelectList(_context.TransactStatuses, "TransactStatusId", "Status", order.TransactStatusId);
			return View(order);
		}

		private bool OrderExists(int id)
		{
			return _context.Orders.Any(e => e.OrderId == id);
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
	}
}