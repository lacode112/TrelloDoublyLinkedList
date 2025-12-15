using System;
using System.Collections.Generic;
public class NoteEntry
{
    public string Title { get; set; }

    // Nội dung chi tiết của ghi chú.
    public string Body { get; set; }
    // lấy thời gian hệ thống khi được tạo.
    public DateTime CreationDate { get; set; }
    //lấy độ ưu tiên
    public int Priority { get; set; }
    //constructor
    public NoteEntry(string title, string body, int priority = 0)
    {
        Title = title;
        Body = body;
        // TỰ ĐỘNG CẬP NHẬT: Gán ngày giờ hệ thống hiện tại cho CreationDate
        this.CreationDate = DateTime.Now;
        Priority = priority;
    }
}