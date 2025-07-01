using IINSwalayan.Data;
using IINSwalayan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IINSwalayan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get featured products
                var featuredProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive)
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(8)
                    .ToListAsync();

                // Get categories
                var categories = await _context.Categories
                    .Include(c => c.Products)
                    .Where(c => c.IsActive && c.Products.Any(p => p.IsActive))
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                // Get banners with data cleaning
                List<Banner> banners = new List<Banner>();

                try
                {
                    var dbBanners = await _context.Banners
                        .Where(b => b.IsActive &&
                                   b.StartDate <= DateTime.Now &&
                                   (b.EndDate == null || b.EndDate >= DateTime.Now))
                        .OrderBy(b => b.Order)
                        .ThenByDescending(b => b.CreatedAt)
                        .ToListAsync();

                    // Clean banner data to prevent console errors
                    foreach (var banner in dbBanners)
                    {
                        var cleanBanner = new Banner
                        {
                            Id = banner.Id,
                            Title = string.IsNullOrWhiteSpace(banner.Title) ? "PROMOSI SPESIAL" : banner.Title,
                            Description = string.IsNullOrWhiteSpace(banner.Description) ? "Dapatkan penawaran terbaik!" : banner.Description,
                            ImageUrl = "", // Force empty to prevent broken image URLs
                            LinkUrl = IsValidUrl(banner.LinkUrl) ? banner.LinkUrl : Url.Action("Products", "Home"),
                            ButtonText = string.IsNullOrWhiteSpace(banner.ButtonText) ? "BELANJA SEKARANG" : banner.ButtonText,
                            BackgroundColor = "", // Let CSS handle the color
                            TextColor = "#FFFFFF",
                            Order = banner.Order,
                            IsActive = banner.IsActive,
                            StartDate = banner.StartDate,
                            EndDate = banner.EndDate,
                            CreatedAt = banner.CreatedAt,
                            UpdatedAt = DateTime.Now
                        };

                        banners.Add(cleanBanner);
                    }

                    // If no banners, create default ones
                    if (!banners.Any())
                    {
                        banners = GetDefaultBanners();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error loading banners from database");
                    banners = GetDefaultBanners();
                }

                ViewBag.FeaturedProducts = featuredProducts;
                ViewBag.Categories = categories;
                ViewBag.Banners = banners;
                ViewData["Title"] = "IIN Swalayan - Belanja Online Mudah & Terpercaya";

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");

                // Provide fallback data
                ViewBag.FeaturedProducts = new List<Product>();
                ViewBag.Categories = new List<Category>();
                ViewBag.Banners = GetDefaultBanners();

                return View();
            }
        }

        // Helper method to validate URLs
        private bool IsValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Check for invalid patterns that cause console errors
            if (url.Contains("ffffff:") ||
                url.Contains("FFFFFF:") ||
                url.Contains("via.placeholder.com") ||
                url.StartsWith("#") ||
                url.Contains(".ext="))
                return false;

            // Basic URL validation
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _);
        }

        // Helper method to get safe default banners
        private List<Banner> GetDefaultBanners()
        {
            return new List<Banner>
            {
                new Banner
                {
                    Id = 1,
                    Title = "PROMOSI SPESIAL",
                    Description = "Dapatkan diskon hingga 50% untuk semua kategori produk!",
                    ImageUrl = "",
                    LinkUrl = Url.Action("Products", "Home") ?? "/Home/Products",
                    ButtonText = "BELANJA SEKARANG",
                    BackgroundColor = "",
                    TextColor = "#FFFFFF",
                    Order = 1,
                    IsActive = true,
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(30),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Banner
                {
                    Id = 2,
                    Title = "GRATIS ONGKIR",
                    Description = "Gratis ongkos kirim untuk pembelian minimal Rp 100.000",
                    ImageUrl = "",
                    LinkUrl = Url.Action("Products", "Home") ?? "/Home/Products",
                    ButtonText = "CEK PRODUK",
                    BackgroundColor = "",
                    TextColor = "#FFFFFF",
                    Order = 2,
                    IsActive = true,
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(30),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }

        public async Task<IActionResult> Products(int? categoryId, string? search, int page = 1)
        {
            try
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive);

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                    var category = await _context.Categories.FindAsync(categoryId.Value);
                    ViewBag.CurrentCategory = category;
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.Contains(search) ||
                                           (p.Description != null && p.Description.Contains(search)));
                    ViewBag.SearchTerm = search;
                }

                int pageSize = 12;
                var totalProducts = await query.CountAsync();
                var products = await query
                    .OrderBy(p => p.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var categories = await _context.Categories
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                ViewBag.Products = products;
                ViewBag.Categories = categories;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
                ViewBag.TotalProducts = totalProducts;
                ViewBag.CategoryId = categoryId;

                ViewData["Title"] = "Semua Produk - IIN Swalayan";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading products page");
                ViewBag.Products = new List<Product>();
                ViewBag.Categories = new List<Category>();
                return View();
            }
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

                if (product == null)
                {
                    return NotFound();
                }

                var relatedProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id && p.IsActive)
                    .Take(4)
                    .ToListAsync();

                ViewBag.Product = product;
                ViewBag.RelatedProducts = relatedProducts;
                ViewData["Title"] = $"{product.Name} - IIN Swalayan";

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading product detail");
                return NotFound();
            }
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Tentang Kami - IIN Swalayan";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Kontak - IIN Swalayan";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Kebijakan Privasi - IIN Swalayan";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}