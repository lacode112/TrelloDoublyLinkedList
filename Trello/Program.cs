using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        TrelloList myKanban = new TrelloList();

        /*Console.WriteLine("=======================================");
        Console.WriteLine("         TEST DOUBLY LINKED LIST       ");
        Console.WriteLine("=======================================");

        // --- ADD ---
        Console.WriteLine("\n--- 1. TEST ADD ---");

        NoteEntry note1 = new NoteEntry("Task 1", "Code LinkedList");
        NoteEntry note2 = new NoteEntry("Task 2", "Code 6 thuật táon bổ sung");
        NoteEntry note3 = new NoteEntry("Task 3", "Code xử lí file json");
        NoteEntry note4 = new NoteEntry("Task 4", "Tạo giao diện Winform");
        NoteEntry note5 = new NoteEntry("Task 5", "Viết báo cáo");

        myKanban.Add(note1);
        myKanban.Add(note2);
        myKanban.Add(note3);
        myKanban.Add(note4);
        myKanban.Add(note5);

        myKanban.PrintForward();
        myKanban.PrintBackward();

        //--- REMOVE ---
        Console.WriteLine("\n--- 2. TEST REMOVE ---");

        // Xóa Node ở giữa
        Console.WriteLine("Xóa Task 3");
        myKanban.Remove("Task 3");
        myKanban.PrintForward();

        // Xóa Node Head
        Console.WriteLine("Xóa Task 1 (Head)");
        myKanban.Remove("Task 1");
        myKanban.PrintForward();

        // Xóa Node Tail
        Console.WriteLine("Xóa Task tail");
        myKanban.Remove("Task 5");
        myKanban.PrintForward();

        //--- UPDATE ---
        Console.WriteLine("\n--- 3. TEST UPDATE ---");
        Console.WriteLine("Cập nhật Task 2");
        bool updated = myKanban.Update("Task 2", "Task 2 - Updated", "Nội dung mới");
        if (updated)
        {
            Console.WriteLine("Cập nhật thành công!");
            myKanban.PrintForward();
        }
        // --- SWAP ---
        Console.WriteLine("\n--- 4. TEST SWAP ---");

        // Đưa danh sách về ban đẩu trước khi remove
        Console.WriteLine("\n--- Danh sách ban đầu ---");
        myKanban = new TrelloList();
        myKanban.Add(note1);
        myKanban.Add(note2);
        myKanban.Add(note3);
        myKanban.Add(note4);
        myKanban.Add(note5);
        myKanban.PrintForward();

        // Swap Head và Tail 
        Console.WriteLine("Swap Task 1 (Head) và Task 5 (Tail)");
        myKanban.Swap("Task 1", "Task 5");
        myKanban.PrintForward();

        // Swap hai Node kế bên
        Console.WriteLine("Swap Task 2 và Task 3");
        myKanban.Swap("Task 2 - Updated", "Task 3");
        myKanban.PrintForward();*/

        Console.WriteLine("=======================================");
        Console.WriteLine("         TEST 6 THUAT TOAN BO SUNG     ");
        Console.WriteLine("=======================================");
        NoteEntry note1 = new NoteEntry("A 1", "Code LinkedList", 4);
        NoteEntry note2 = new NoteEntry("D 2", "Code 6 thuật táon bổ sung", 5);
        NoteEntry note3 = new NoteEntry("C 3", "Code xử lí file json", 1);
        NoteEntry note4 = new NoteEntry("Task 4", "Tạo giao diện Winform", 9);
        NoteEntry note5 = new NoteEntry("B 5", "Viết báo cáo", 1);

        myKanban.Add(note1);
        myKanban.Add(note2);
        myKanban.Add(note3);
        myKanban.Add(note4);
        myKanban.Add(note5);

        //--- REVERSE ---
        Console.WriteLine("\n--- 1. TEST REVERSE ---");
        myKanban.Reverse();
        myKanban.PrintForward();

        //--- SEARCH ---
        Console.WriteLine("\n--- 2. TEST SEARCH (Chu 'CODE'---");
        var result = myKanban.SearchKeyWord("CODE");
        foreach (var note in result)
            Console.WriteLine(note.Data.Title + ": " + note.Data.Body);

        //--- PIN TO HEAD ---
        Console.WriteLine("\n--- 3. TEST PIN TO HEAD (Pin 'C 3'---");
        myKanban.PinToHead("C 3");
        myKanban.PrintForward();

        //--- SORT ---
        Console.WriteLine("\n--- 4. TEST SORT BY PRIORITY ---");
        myKanban.SortByPriority(true);
        myKanban.PrintForward();
        Console.WriteLine("\n--- 4. TEST SORT BY DATE ---");
        myKanban.SortByDate(true);
        myKanban.PrintForward();
        Console.WriteLine("\n--- 4. TEST SORT BY TITLE ---");
        myKanban.SortByTitle(true);
        myKanban.PrintForward();

        //--- REMOVE ALL ---
        Console.WriteLine("\n--- 5. TEST CLEAR ALL ---");
        myKanban.RemoveAll();
    }
}


