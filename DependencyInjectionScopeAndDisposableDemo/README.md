# 说明

本项目用于测试依赖注入容器管理对象生命周期的情况，结论如下：

使用`AddTransient()`注册的类型，容器创建对象后：

1. 实现了`IDisposable`接口的对象，调用`System.GC.Collect()`不会销毁，会与容器一起释放；
2. 未实现`IDisposable`接口的对象，调用`System.GC.Collect()`会销毁；
3. 在`Controller`中使用构造函数注入或者`[FromServies]`注入的对象，不用关心生命周期，系统会帮助管理。
4. 用`ApplicationServices.GetService()`创建示例时，最好用`Scope`限定生命周期范围。
    ```cs
    using var scope = app.ApplicationServices.CreateScope();
    var orderService = scope.ServiceProvider.GetService<IOrderService>();
    ```