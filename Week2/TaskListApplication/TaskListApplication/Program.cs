internal class Program
{
    private static void Main(string[] args)
    {
        int ch = 0;
        List<string> taskList = new List<string>();

        Console.WriteLine("  $$$   Simple Task List Application   $$$  ");
        while (ch != 5)
        {
            Console.WriteLine("""
            -------------------------------------------
            1. Create Task
            2. Read Tasks
            3. Update Task
            4. Delete Task
            5. Exit
            Enter your choice : 
            """);
            ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    Console.WriteLine("Enter the Task Description :");
                    taskList.Add(Console.ReadLine());
                    Console.WriteLine("Task Added Successfully....");
                    break;
                case 2:
                    for (int i = 0; i < taskList.Count; i++)
                    {
                        Console.WriteLine($"Task {i + 1}. {taskList[i]}");
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter which task do you want to modify : ");
                    var task = Console.ReadLine();
                    int index = taskList.IndexOf(task);
                    if (index != -1)
                    {
                        Console.WriteLine("Enter the new task name : ");
                        string newTask = Console.ReadLine();
                        taskList[index] = newTask;
                        Console.WriteLine("Task updated Successfully.....");
                    }
                    else
                    {
                        Console.WriteLine("Task not found !!!");
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter which task do you want to delete : ");
                    string task1 = Console.ReadLine();
                    if (taskList.Contains(task1))
                    {
                        taskList.Remove(task1);
                        Console.WriteLine("Task deleted successfully...");
                    }
                    else
                    {
                        Console.WriteLine("Task not found !!!");
                    }
                    break;
                case 5:
                    Console.WriteLine("Thank you for using System.....");
                    return;
                default:
                    Console.WriteLine("Invalid choice !!!");
                    break;
            }
        }
    }
}