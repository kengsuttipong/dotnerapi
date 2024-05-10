using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CategoryController(ApplicationDbContext context,
                            IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

        // ฟังก์ชันสำหรับการดึงข้อมูลสินค้าทั้งหมด
    // GET: /api/Category
    [HttpGet]
    public ActionResult<category> GetCategorys()
    {
        // LINQ สำหรับการดึงข้อมูลจากตาราง Categorys ทั้งหมด
        // var Categorys = _context.Categorys.ToList();

        // แบบอ่านที่มีเงื่อนไข
        // var Categorys = _context.Categorys.Where(p => p.unit_price > 45000).ToList();

        // แบบเชื่อมกับตารางอื่น Categorys เชื่อมกับ categories
        var Categorys = _context.categories.Select(c => new {
                    c.category_id,
                    c.category_status,
                    c.category_name
                }).ToList();

        // ส่งข้อมูลกลับไปให้ผู้ใช้งาน
        return Ok(Categorys);
    }

    // ฟังก์ชันสำหรับการดึงข้อมูลสินค้าตาม id
    // GET: /api/Category/{id}
    [HttpGet("{id}")]
    public ActionResult<category> GetCategory(int id)
    {
        // LINQ สำหรับการดึงข้อมูลจากตาราง Categorys ตาม id
        var Category = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (Category == null)
        {
            return NotFound();
        }

        // ส่งข้อมูลกลับไปให้ผู้ใช้งาน
        return Ok(Category);
    }

    // ฟังก์ชันสำหรับการเพิ่มข้อมูลสินค้า
    // POST: /api/Category
    [HttpPost]
    public async Task<ActionResult<category>> CreateCategory([FromForm] category Category, IFormFile image)
    {
        // เพิ่มข้อมูลลงในตาราง Categorys
        _context.categories.Add(Category);

        _context.SaveChanges();

        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(Category);
    }

    // PUT: /api/Category/{id}
    [HttpPut("{id}")]
    public ActionResult<category> UpdateCategory(int id, category Category)
    {
        // ดึงข้อมูลสินค้าตาม id
        var existingCategory = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (existingCategory == null)
        {
            return NotFound();
        }

        // แก้ไขข้อมูลสินค้า
        existingCategory.category_name = Category.category_name;
        existingCategory.category_status = Category.category_status;
        existingCategory.category_id = Category.category_id;

        // บันทึกข้อมูล
        _context.SaveChanges();

        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(existingCategory);
    }

    // ฟังก์ชันสำหรับการลบข้อมูลสินค้า
    // DELETE: /api/Category/{id}
    [HttpDelete("{id}")]
    public ActionResult<category> DeleteCategory(int id)
    {
        // ดึงข้อมูลสินค้าตาม id
        var Category = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (Category == null)
        {
            return NotFound();
        }

        // ลบข้อมูล
        _context.categories.Remove(Category);
        _context.SaveChanges();

        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(Category);
    }
}