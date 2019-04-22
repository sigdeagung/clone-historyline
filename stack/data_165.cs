   interface IInterfaceWithGenericMethod
    {
        void TheInterfaceMethod<T>();
    }
     Type selectedType = typeSelectedByEnum;
        MethodInfo method = selectedType.GetMethod("TheInterfaceMethod");
        object obj = Activator.CreateInstance(selectedType);
        method.Invoke(obj, null);
           Type selectedType = typeSelectedByEnum;
        object obj = Activator.CreateInstance(selectedType);
        IAwesomeInterface converted = obj as IAwesomeInterface;
        converted.TheInterfaceMethod();