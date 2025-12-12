using System;
using System.Collections.Generic;
public class NoteEntry
{
    // Tiêu đề của ghi chú để tìm kiếm, xóa, và swap.
    public string Title { get; set; }

    // Nội dung chi tiết của ghi chú.
    public string Body { get; set; }
    //constructor
    public NoteEntry(string title, string body)
    {
        Title = title;
        Body = body;
    }
}