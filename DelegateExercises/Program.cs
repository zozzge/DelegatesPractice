
DataStore<Student> store = new DataStore<Student>(15);

store.Add(new Student() { Id = 1, Name = "Tevfik", Age = 52 });
store.Add(new Student() { Id = 2, Name = "Zeynep", Age = 32 });
store.Add(new Student() { Id = 3, Name = "Hande", Age = 12 });
store.Add(new Student() { Id = 4, Name = "Ahmet", Age = 22 });
store.Add(new Student() { Id = 5, Name = "Mehmet", Age = 33 });
store.Add(new Student() { Id = 6, Name = "Özge", Age = 25 });
store.Add(new Student() { Id = 7, Name = "Deniz", Age = 44 });
store.Add(new Student() { Id = 8, Name = "Ayşe", Age = 34 });
store.Add(new Student() { Id = 9, Name = "Zeynep", Age = 35 });
store.Add(new Student() { Id = 10, Name = "Zeynep", Age = 36 });
store.Add(new Student() { Id = 11, Name = "Zeynep", Age = 37 });
store.Add(new Student() { Id = 12, Name = "Zeynep", Age = 38 });
store.Add(new Student() { Id = 13, Name = "Zeynep", Age = 39 });


Student student;

student = store.Where(s => s.Name == "Zeynep").FirstOrDefault();
Console.WriteLine(student.Name + " --> " + student.Age);

student = store.Where(s => s.Age == 12).FirstOrDefault();
Console.WriteLine(student.Name + " --> " + student.Age);

List<Student> studentsWithZeynepName = store.Where(s => s.Name == "Zeynep").ToList();
Console.WriteLine("Students with the name Zeynep:");

foreach (var studentWithName in studentsWithZeynepName)
{
    Console.WriteLine(studentWithName.Name + " --> " + studentWithName.Age);
}

List<Student> studentsWithAge12 = store.Where(s => s.Age == 12).ToList();
Console.WriteLine("\nStudents with age 12:");

foreach (var studentWithAge in studentsWithAge12)
{
    Console.WriteLine(studentWithAge.Name + " --> " + studentWithAge.Age);
}

var pagedStudent = store.Where(n => n.Name == "Zeynep").Skip(2).Take(2);
foreach (var studentWithAge in pagedStudent)
{
    Console.WriteLine("Paged Result: " + studentWithAge.Name + " --> " + studentWithAge.Age);
}

public class DataStore<T> where T : class
{
    public delegate bool FindDelegate(T item);
    
    private T[] items;
    private int itemIndex;
    private FindDelegate findDelegate;
    private int skipCount;

    public DataStore(int storeSize)
    {
        items = new T[storeSize];
        itemIndex = 0;
    }

    public void Add(T item)
    {
        items[itemIndex] = item;
        itemIndex++;
    }

    public DataStore<T> Where(FindDelegate findDelegate)
    {
        this.findDelegate = findDelegate;

        return this;
    }

    public T FirstOrDefault(T defaultItem = null)
    {
        for (int i = 0; i < itemIndex; i++)
        {
            if (findDelegate(items[i]))
                return items[i];
        }

        return defaultItem;
    }

    public DataStore<T> Skip(int skipCount)
    {
        this.skipCount = skipCount;

        return this;
    }

    public List<T> Take(int takeCount)
    {
        List<T> tempList = new List<T>();

        for (int i = 0; i < itemIndex; i++)
        {
            if (findDelegate(items[i]))
                tempList.Add(items[i]);
        }

        List<T> takeList = new List<T>();   

        if (tempList.Count > 0)
        {
            for (int i = skipCount; i < skipCount + takeCount; i++)
            {
                takeList.Add(tempList[i]);
            }
        }

        return takeList;
    }

     
    
    public List<T> ToList()
    {
        List<T> matchingItems = new List<T>();
        for (int i = 0; i < itemIndex; i++)
        {
            if (findDelegate(items[i]))
            {
                matchingItems.Add(items[i]);
            }
        }
        return matchingItems;
    }



}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
