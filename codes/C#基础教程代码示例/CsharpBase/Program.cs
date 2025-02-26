using System.Runtime.InteropServices;
using System.Xml;



//DefineVariable();

#region 变量定义与基础类型



static void DefineVariable()
{
    var age = 18;
    var name = "小明";
    var money = 12.5;
    Console.WriteLine($"{name} 今年 {age} 岁, 拥有 {money} 元");

    byte a = 1;
    Console.WriteLine($"类型: {a.GetType()}");
    Console.WriteLine($"字节数: {Marshal.SizeOf(a)}");
    Console.WriteLine($"取值范围: {byte.MinValue} ~ {byte.MaxValue}");


    int[] arr = new int[5];// 定义一个长度为5的整型数组
    var arr1 = new int[] { 1, 2, 3, 4, 5 }; // 数组初始化
    int[] arr3 = [1, 2, 3, 4, 5]; // 这是C#的最新数组初始化写法，仅限于较新版本
}

#endregion



#region 函数定义和使用

int f(int x)=> x;
Console.WriteLine(f(1));

#endregion