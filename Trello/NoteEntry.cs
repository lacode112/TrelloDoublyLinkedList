using System;
using System.Collections.Generic;
public class NoteEntry
{
    // Tiêu đề của ghi chú để tìm kiếm, xóa, và swap.
    public string Title { get; set; }

    // Nội dung chi tiết của ghi chú.
    public string Body { get; set; }
    // lấy thời gian hệ thống khi được tạo.
    public DateTime CreationDate { get; set; } 
    //constructor
    public NoteEntry(string title, string body)
    {
        Title = title;
        Body = body;
        // TỰ ĐỘNG CẬP NHẬT: Gán ngày giờ hệ thống hiện tại cho CreationDate
        this.CreationDate = DateTime.Now;
    }
}