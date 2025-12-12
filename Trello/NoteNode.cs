using System;
public class NoteNode
{
    // Lưu trữ dữ liệu tạo ở bước trước.
    public NoteEntry Data;

    // Con trỏ trỏ đến Node tiếp theo
    public NoteNode Next;

    // Con trỏ trỏ đến Node phía trước 
    //Doubly Linked List
    public NoteNode Prev;

    // Khi tạo một Node, nó chứa dữ liệu và chưa liên kết với bất kỳ Node nào.
    public NoteNode(NoteEntry data)
    {
        this.Data = data;
        // Ban đầu, Next và Prev đều là null.
        this.Next = null;
        this.Prev = null;
    }
}