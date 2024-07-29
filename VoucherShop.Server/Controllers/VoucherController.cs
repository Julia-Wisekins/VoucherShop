using Microsoft.AspNetCore.Mvc;
using VoucherShop.Server.Data;
using VoucherShop.Server.Interfaces;
using VoucherShop.Server.Model;

namespace VoucherShop.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly ILogger<VoucherController> _logger;
        private readonly IProductService<Voucher> _voucherService;
        private readonly IRepository<CartItem> _cartRepo;
        private readonly IRepository<CheckOut> _checkoutRepo;

        public VoucherController(ILogger<VoucherController> logger, IProductService<Voucher> voucherService
            , IRepository<CartItem> cartRepo, IRepository<CheckOut> checkoutRepo)
        {
            _logger = logger;
            _voucherService = voucherService;
            _cartRepo = cartRepo;
            _checkoutRepo = checkoutRepo;
        }

        [HttpGet]
        [Route("ListVouchers")]
        public IEnumerable<Voucher> ListVouchers()
        {
            return _voucherService.ListProducts();
        }

        [HttpPost]
        [Route("AddToCart")]
        public ActionResult<Voucher> AddToCart(CartVoucher voucher)
        {
            try
            {
                if (voucher == null
                    || voucher.Voucher.Amount == 0 
                    || _checkoutRepo.Search(voucher.CartId.ToString()).Result.Any())
                    return BadRequest();

                if(voucher.CartId == -1)
                {
                    voucher.CartId = _voucherService.CreateCart();
                }

                _cartRepo.Add(new CartItem() { Id = voucher.CartId, Voucher = voucher.Voucher });
                List<Voucher> vouchers = new();
                foreach(var cartItem in _cartRepo.GetById(voucher.CartId).Result)
                {
                    vouchers.Add(cartItem.Voucher);
                }
                _voucherService.UpdateCart(voucher.CartId, vouchers);

                return Created(Request.Host.ToString(), voucher.CartId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Checkout")]
        public ActionResult Checkout(CartVoucher cartVoucher)
        {
            try
            {
                if (_checkoutRepo.Search(cartVoucher.CartId.ToString()).Result.Any())
                    return BadRequest();

                var checkoutId = _voucherService.Checkout(cartVoucher.CartId);
                _checkoutRepo.Add(new CheckOut() { Id = checkoutId, CartID = cartVoucher.CartId, Time = DateTime.UtcNow, TotalPrice = _cartRepo.GetById(cartVoucher.CartId).Result.Sum(x => x.Voucher.Amount)});
                return checkoutId != -1 ? Accepted() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
