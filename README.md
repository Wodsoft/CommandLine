# CommandLine 1.0
CommandLine���԰��������߿��ٹ���������������ڸ��ֿ���̨������ʹ�á�

## ���ʹ��
### ��д����

    [Command("add", Descrption = "�ӷ�����")]
    public class AddCommand : ICommand
    {
        [CommandParameter("left", IsDefault = true, IsRequired = true, Description = "�Ӻ���ߵ�ֵ", Order = 0)]
        public double Left { get; set; }
    
        [CommandParameter("right", IsDefault = true, IsRequired = true, Description = "�Ӻ��ұߵ�ֵ", Order = 1)]
        public double Right { get; set; }
    
        public void Invoke(ICommandContext context)
        {
            context.WriteLine((Left + Right).ToString());
        }
    }
### ����̨��������

    class Program
    {
        static void Main(string[] args)
        {
            //ʵ��������̨����������
            ConsoleCommandContext context = new ConsoleCommandContext(null);
            //ʵ���������ṩ��
            CommandProvider provider = new CommandProvider();
            //�������
            provider.AddCommand<AddCommand>();
            provider.AddCommand<HelpCommand>();
            context.CommandProvider = provider;
            //ʵ�������������
            CommandParser parser = new CommandParser(context, provider);
            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd.Trim().ToLower() == "quit")
                    break;
                //������ִ������
                parser.ParseAndRun(cmd);
            }
        }
    }
### ����̨ʹ��
![Demo](doc/images/console.png)
## ���֤
[MIT](LICENSE)