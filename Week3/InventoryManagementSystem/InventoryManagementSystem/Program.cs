class  Item
{
    public int ID;
    public string Name;
    public double Price;
    public int Quantity;

    public Item(int id, string name, double price, int quantity)
    {
        ID = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    public override string ToString()
    {
        return $"{ID} - {Name} - Price : {Price} - Quantity: {Quantity}";
    }
}

class Inventory
{
    List<Item> items;
    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine("Item added successfully!");
    }

    public void DisplayItems()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public void SearchItem(int id)
    {
        Item item=items.Find(item => item.ID == id);
        if(item == null)
        {
            Console.WriteLine("Item not found");
        }
        else
        {
            Console.WriteLine(item);
        }
    }

    public void UpdateItem(Item uitem)
    {
        int id=items.FindIndex(item=>item.ID == uitem.ID);
        if(id == -1)
        {
            Console.WriteLine("Item not found");
        }
        else
        {
            items[id] = uitem;
            Console.WriteLine("Item updated successfully!");
        }
    }
    public void RemoveItem(int id)
    {
        Item item = items.Find(item => item.ID==id);
        if (item == null)
        {
            Console.WriteLine("Item not found");
        }
        else
        {
            items.Remove(item);
            Console.WriteLine("Item deleted successfully!");
        }
       
    }

    public int GenerateNextID()
    {
        int id=0;
        do
        {
            id++;

        }while(items.Exists(item=>item.ID==id));
        return id;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        Console.WriteLine("Inventory Management System");
        while (true)
        {
            Console.WriteLine("""

                1. Add a new item
                2. Display all items
                3. Find an item by ID
                4. Update an item's information
                5. Delete an item
                6. Exit
                """);
            Console.Write("Enter your choice : ");
            int choice =Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    AddItem(inventory);
                    break;
                case 2:
                    DisplayItems(inventory);
                    break;
                case 3:
                    SearchItem(inventory);
                    break;
                case 4:
                    UpdateItem(inventory);
                    break;
                case 5:
                    RemoveItem(inventory);
                    break;
                case 6:
                    Console.WriteLine("Thank You for using the system!");
                    return;
                default: 
                    Console.WriteLine("""
                        Invalid choice !!!
                        Please enter a number between 1 to 6.
                        """);
                    break;
            }

        }
    }

    private static void UpdateItem(Inventory inventory)
    {
        int id,quantity;
        string name;
        double price;
        Console.Write("Enter item Id to update : ");
        id= Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter updated item name : ");
        name= Convert.ToString(Console.ReadLine());
        Console.Write("Enter updated item price : ");
        price= Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter updated item quantity : ");
        quantity=Convert.ToInt32(Console.ReadLine());
        Item item = new Item(id, name, price, quantity);
        inventory.UpdateItem(item);
    }

    private static void RemoveItem(Inventory inventory)
    {
        int id;
        Console.Write("Enter item ID to delete : ");
        id = Convert.ToInt32(Console.ReadLine());
        inventory.RemoveItem(id);
    }

    private static void SearchItem(Inventory inventory)
    {
        int id;
        Console.Write("Enter item ID to find : ");
        id = Convert.ToInt32(Console.ReadLine());
        inventory.SearchItem(id);
    }

    private static void AddItem(Inventory inventory)
    {
        int id;
        string name;
        double price;
        int quantity;
        id=inventory.GenerateNextID();
        Console.Write("Enter item name : ");
        name = Console.ReadLine();
        Console.Write("Enter item price : ");
        price=Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter item quantity : ");
        quantity=Convert.ToInt32(Console.ReadLine());
        Item item=new Item(id,name,price,quantity);
        inventory.AddItem(item);
    }

    private static void DisplayItems(Inventory inventory)
    {
        inventory.DisplayItems();
    }
}