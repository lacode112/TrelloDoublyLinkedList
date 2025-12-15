using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;
public class TrelloList
{
    public NoteNode head;
    public NoteNode tail;

    // Constructor đơn giản
    public TrelloList()
    {
        head = null;
        tail = null;
    }

    // Thuộc tính để lấy tất cả các ghi chú lalala
    public List<NoteEntry> GetAllNotes()
    {
        var notes = new List<NoteEntry>();
        NoteNode current = head;
        while (current != null)
        {
            notes.Add(current.Data);
            current = current.Next;
        }
        return notes;
    }

    // Add
    public void Add(NoteEntry data)
    {
        NoteNode newNode = new NoteNode(data);

        if (head == null)
        {
            // Danh sách rỗng
            head = newNode;
            tail = newNode;
        }
        else
        {
            // 1. Node mới Prev = Tail cũ.
            newNode.Prev = tail;
            // 2. Tail cũ Next = Node mới.
            tail.Next = newNode;
            // 3. Tail mới = Node mới.
            tail = newNode;
        }
    }

    // find
    public NoteEntry Find(string title)
    {
        NoteNode current = head;
        while (current != null)
        {
            // Tìm kiếm không phân biệt chữ hoa/chữ thường 
            if (string.Equals(current.Data.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                return current.Data;
            }
            current = current.Next;
        }
        return null; // Không tìm thấy.
    }


    public void Remove(string title)
    {
        // 1. Kiểm tra danh sách rỗng
        if (head == null) return;

        // 2. Tìm Node cần xóa 
        NoteNode nodeToRemove = null;
        NoteNode current = head;
        while (current != null)
        {
            if (string.Equals(current.Data.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                nodeToRemove = current;
                break;
            }
            current = current.Next;
        }

        if (nodeToRemove == null) return; // Không tìm thấy 

        // Nếu node muốn xoá là head
        if (nodeToRemove == head)
        {
            head = head.Next; // Di chuyển head sang node kế tiếp

            if (head != null)
            {
                head.Prev = null; // bỏ prev cho node head mới
            }
            else
            {
                tail = null; // Danh sách rỗng, cập nhật tail
            }
        }
        //Nếu node muốn xoá là tail
        else if (nodeToRemove == tail)
        {
            tail = tail.Prev; // Di chuyển Tail về Node prev

            if (tail != null)
            {
                tail.Next = null; // bỏ next của tail mới
            }
        }
        //Nếu node muốn xoá nằm ơ giữa
        else
        {
            // 1. Nối Prev với Next của Node bị xóa (con trỏ next của node đằng trước chuya63n thành node đằng sau node bị xoá)
            nodeToRemove.Prev.Next = nodeToRemove.Next;

            // 2. Nối Next với Prev của Node bị xóa (con trỏ prev của node đằng sau chuya63n thành node đằng trước node bị xoá)
            nodeToRemove.Next.Prev = nodeToRemove.Prev;
        }

        nodeToRemove.Prev = null;
        nodeToRemove.Next = null;
    }

    //update
    public bool Update(string oldTitle, string newTitle, string newBody)
    {
        NoteNode node = FindNode(oldTitle);
        if (node == null)
        {
            return false; // Không tìm thấy node cần sửa
        }

        // Kiểm tra xem newTitle có trùng với task khác không
        if (!string.Equals(oldTitle, newTitle, StringComparison.OrdinalIgnoreCase))
        {
            NoteNode duplicate = FindNode(newTitle);
            if (duplicate != null)
            {
                throw new InvalidOperationException("Title already exists.");
            }
        }

        // Cập nhật dữ liệu
        node.Data.Title = newTitle;
        node.Data.Body = newBody;

        return true;
    }

    private NoteNode FindNode(string title)
    {
        NoteNode current = head;
        while (current != null)
        {
            if (string.Equals(current.Data.Title, title, StringComparison.OrdinalIgnoreCase))
                return current; // Trả về cấu trúc Node
            current = current.Next;
        }
        return null;
    }

    // Hoán đổi vị trí hai Node dựa trên tiêu đề
    public void Swap(string title1, string title2)
    {
        NoteNode node1 = FindNode(title1);
        NoteNode node2 = FindNode(title2);

        if (node1 == null || node2 == null)
            throw new InvalidOperationException("One or both notes not found.");

        if (node1 == node2) return; // Cùng node thì không cần swap

        // Lưu các con trỏ liên quan
        NoteNode node1Prev = node1.Prev;
        NoteNode node1Next = node1.Next;
        NoteNode node2Prev = node2.Prev;
        NoteNode node2Next = node2.Next;

        //2 node kề nhau
        if (node1.Next == node2) // node1 -> node2
        {
            node1.Next = node2Next;
            node1.Prev = node2;
            node2.Next = node1;
            node2.Prev = node1Prev;

            if (node1.Next != null) node1.Next.Prev = node1;
            if (node2.Prev != null) node2.Prev.Next = node2;
        }
        else if (node2.Next == node1) // node2 -> node1
        {
            node2.Next = node1Next;
            node2.Prev = node1;
            node1.Next = node2;
            node1.Prev = node2Prev;

            if (node2.Next != null) node2.Next.Prev = node2;
            if (node1.Prev != null) node1.Prev.Next = node1;
        }
        else // 2 node không kề nhau
        {
            node1.Prev = node2Prev;
            node1.Next = node2Next;
            node2.Prev = node1Prev;
            node2.Next = node1Next;

            if (node1.Prev != null) node1.Prev.Next = node1;
            if (node1.Next != null) node1.Next.Prev = node1;
            if (node2.Prev != null) node2.Prev.Next = node2;
            if (node2.Next != null) node2.Next.Prev = node2;
        }

        // Cập nhật head và tail nếu cần
        if (head == node1) head = node2;
        else if (head == node2) head = node1;

        if (tail == node1) tail = node2;
        else if (tail == node2) tail = node1;
    }


    //hàm in danh sách xuôi
    public void PrintForward()
    {
        Console.Write("DANH SÁCH (Xuôi): ");
        NoteNode current = head;
        while (current != null)
        {
            Console.WriteLine($"\n[TITLE: {current.Data.Title}] - [BODY: {current.Data.Body}] (CREATED: {current.Data.CreationDate.ToString("HH:mm:ss.fff")}) - Priority: {current.Data.Priority} -> ");
            current = current.Next;
        }
        Console.WriteLine("NULL (Tail)");
    }

    // Hàm in danh sách ngược 
    public void PrintBackward()
    {
        Console.Write("DANH SÁCH (Ngược): ");
        NoteNode current = tail;
        while (current != null)
        {
            Console.WriteLine($"\n[TITLE: {current.Data.Title}] - [BODY: {current.Data.Body}] (CREATED: {current.Data.CreationDate.ToString("HH:mm:ss.fff")}) - Priority: {current.Data.Priority} <- ");
            current = current.Prev;
        }
        Console.WriteLine("NULL (Head)");
    }

    //6 thuật toán bổ sung
    //1. Đảo ngược danh sách
    public void Reverse()
    {
        if (head == null || head.Next == null)
            return; //Danh sách rỗng hoặc chỉ có một Node --> Không cần đảo ngược

        NoteNode current = head;
        NoteNode temp = null;

        while (current != null)
        {
            temp = current.Prev; // lưu tạm Node trước
            current.Prev = current.Next; // Next thành Prev
            current.Next = temp; // Prev thành Next
            current = current.Prev; // trỏ sang Node kế tiếp
        }

        // đổi head - tail
        if (temp != null)
        {
            tail = head;
            head = temp.Prev;
        }
    }

    //2. Thuật toán Sort By Title/Date
    public void SortByTitle(bool acseding) //Selection Sort 
    {
        if (head == null || head.Next == null)
            return;

        for (NoteNode current = head; current.Next != null; current = current.Next)
        {
            NoteNode min = current;
            for (NoteNode scanner = current.Next; scanner != null; scanner = scanner.Next)
            {
                string minTitle = min.Data.Title;
                string scannerTitle = scanner.Data.Title;

                if ((acseding && (String.Compare(scannerTitle, minTitle, StringComparison.CurrentCultureIgnoreCase) < 0))
                    || (!acseding && (String.Compare(scannerTitle, minTitle, StringComparison.OrdinalIgnoreCase) > 0)))
                    min = scanner;
            }
            if (min != current)
            {
                //Swap Node
                Swap(min.Data.Title, current.Data.Title);
                current = min; // cập nhật current sau khi swap
            }
        }
    }
    public void SortByDate(bool acsedingg)//Bubble Sort 
    {
        if (head == null || head.Next == null)
            return;
        bool swapped = true;
        while (swapped)
        {
            NoteNode current = head;
            swapped = false;
            while (current.Next != null)
            {
                NoteNode next = current.Next;
                DateTime currentDate = current.Data.CreationDate;
                DateTime nextDate = next.Data.CreationDate;
                if ((acsedingg && (currentDate > nextDate))
                    || (!acsedingg && (currentDate < nextDate)))
                {
                    if (current.Prev != null)
                        current.Prev.Next = next;
                    else
                        head = next; //B thành head
                    next.Prev = current.Prev;
                    if (next.Next != null)
                        next.Next.Prev = current;
                    else
                        tail = current; // A thành tail
                    current.Next = next.Next;
                    next.Next = current;
                    current.Prev = next;
                    // Đánh dấu đã swap
                    swapped = true;
                }
                else
                {
                    current = current.Next;
                }
            }
            if (!swapped)
                break;
        }
    }
    //3. Tìm kiếm theo từ khóa
    public List<NoteNode> SearchKeyWord(string keyword) //SeqSearch
    {
        List<NoteNode> results = new List<NoteNode>();
        if (head == null || string.IsNullOrEmpty(keyword))
            return results;
        for (NoteNode current = head; current != null; current = current.Next)
        {
            string title = current.Data.Title;
            string body = current.Data.Body;
            if (title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || body.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                results.Add(current);
        }
        return results;
    }
    //4. Ghim Node lên đầu danh sách
    public void PinToHead(string title)
    {
        NoteNode current = head;
        while (current != null && !current.Data.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            current = current.Next;
        if (current != null && current != head)
        {
            current.Prev.Next = current.Next;
            if (current.Next != null)
                current.Next.Prev = current.Prev;
            else
                tail = current.Prev; //node ở cuối
            current.Next = head;
            head.Prev = current;
            current.Prev = null;
            head = current;
        }
    }

    //5. Thuật toán Xóa tất cả 
    public void RemoveAll()
    {
        NoteNode current = head;
        while (current != null)
        {
            current.Next = null;
            current.Prev = null;
            current.Data = null;
            current = current.Next;
        }
        head = tail = null;
    }

    //6. Thuật toán Sort By Priority
    public void SortByPriority(bool acseding) //Insertion Sort
    {
        if (head == null || head.Next == null)
            return;
        NoteNode current = head.Next;
        while (current != null)
        {
            NoteNode nextCurrent = current.Next;
            NoteNode scan = current;
            while (scan.Prev != null && ((acseding && scan.Prev.Data.Priority > scan.Data.Priority) ||
                    (!acseding && scan.Prev.Data.Priority < scan.Data.Priority)))
            {
                NoteNode preScan = scan.Prev;
                if (preScan.Prev != null)
                    preScan.Prev.Next = scan;
                else
                    head = scan;
                scan.Prev = preScan.Prev;
                if (scan.Next != null)
                    scan.Next.Prev = preScan;
                else
                    tail = preScan;
                preScan.Next = scan.Next;
                scan.Next = preScan;
                preScan.Prev = scan;
            }
            current = nextCurrent;
        }
    }
}