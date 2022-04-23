using System.Reflection;
using Castle.DynamicProxy;
using Sparrow.Wizard.Engine;
using Sparrow.Wizard.Host;

var builder = new DefaultProxyBuilder();

var type = typeof(IWizardEngine);

var tb = builder.ModuleScope.DefineType(
    true,
    type.Name.Substring(1),
    TypeAttributes.Public |
    TypeAttributes.SpecialName |
    TypeAttributes.Class |
    TypeAttributes.Abstract |
    TypeAttributes.AutoLayout);

tb.AddInterfaceImplementation(type);

foreach (var method in type.GetMethods())
{
    var mb = tb.DefineMethod(
        method.Name,
        MethodAttributes.Abstract | MethodAttributes.SpecialName | MethodAttributes.Public| MethodAttributes.Virtual);

    mb.SetReturnType(method.ReturnType);

    var parameters = method.GetParameters();

    mb.SetParameters(parameters.Select(m => m.ParameterType).ToArray());
}

var implType = tb.CreateType();

if (type.IsAssignableFrom(implType))
{
    Console.WriteLine(implType);
}

var options = new ProxyGenerationOptions();

options.Selector = new MyInterceptorSelector();

var generator = new ProxyGenerator();

var obj = generator.CreateInterfaceProxyWithoutTarget(type, options);

if (obj is IWizardEngine engine)
{
    var list = await engine.GetItemsAsync(CancellationToken.None);
}

Console.ReadKey();