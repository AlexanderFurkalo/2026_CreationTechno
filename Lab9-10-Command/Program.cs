using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10_Command
{
    // "Command" : абстрактна Команда
    abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }
    // "ConcreteCommand" : конкретна команда
    class InsertCommand : Command
    {
        private TextEditor editor;
        private string text;

        public InsertCommand(TextEditor editor, string text)
        {
            this.editor = editor;
            this.text = text;
        }

        public override void Execute()
        {
            editor.Insert(text);
        }

        public override void Undo()
        {
            editor.Delete(text.Length);
        }
    }

    class DeleteCommand : Command
    {
        private TextEditor editor;
        private int length;
        private string deletedText;

        public DeleteCommand(TextEditor editor, int length)
        {
            this.editor = editor;
            this.length = length;
        }

        public override void Execute()
        {
            int start = editor.Text.Length - length;
            if (start < 0) start = 0;

            deletedText = editor.GetText(start, editor.Text.Length - start);
            editor.Delete(length);
        }

        public override void Undo()
        {
            editor.Insert(deletedText);
        }
    }

    class CopyCommand : Command
    {
        private TextEditor editor;
        private int start;
        private int length;

        public CopyCommand(TextEditor editor, int start, int length)
        {
            this.editor = editor;
            this.start = start;
            this.length = length;
        }

        public override void Execute()
        {
            editor.Clipboard = editor.GetText(start, length);
            Console.WriteLine("Copied: " + editor.Clipboard);
        }

        public override void Undo()
        {
            // Copy не змінює стан тексту, тому Undo нічого не робить
        }
    }

    // "Receiver" : отримувач
    class TextEditor
    {
        public string Text { get; private set; } = "";
        public string Clipboard { get; set; } = "";

        public void Insert(string text)
        {
            Text += text;
        }

        public void Delete(int length)
        {
            if (length > Text.Length)
                length = Text.Length;

            Text = Text.Substring(0, Text.Length - length);
        }

        public string GetText(int start, int length)
        {
            return Text.Substring(start, length);
        }

        public void Show()
        {
            Console.WriteLine("Text: " + Text);
        }
    }
    // "Invoker" : той, що викликає
    class User
    {
        private List<Command> history = new List<Command>();
        private int current = 0;

        public void ExecuteCommand(Command command)
        {
            command.Execute();

            // Якщо після Undo виконано нову дію, очищаємо "redo"
            if (current < history.Count)
            {
                history.RemoveRange(current, history.Count - current);
            }

            history.Add(command);
            current++;
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\nUndo " + levels);

            for (int i = 0; i < levels; i++)
            {
                if (current > 0)
                {
                    history[--current].Undo();
                }
            }
        }

        public void Redo(int levels)
        {
            Console.WriteLine("\nRedo " + levels);

            for (int i = 0; i < levels; i++)
            {
                if (current < history.Count)
                {
                    history[current++].Execute();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TextEditor editor = new TextEditor();
            User user = new User();

            user.ExecuteCommand(new InsertCommand(editor, "Hello "));
            user.ExecuteCommand(new InsertCommand(editor, "World"));
            editor.Show();

            user.ExecuteCommand(new DeleteCommand(editor, 5));
            editor.Show();

            user.ExecuteCommand(new CopyCommand(editor, 0, 5));

            user.Undo(2);
            editor.Show();

            user.Redo(2);
            editor.Show();

            Console.ReadKey();
        }
    }
}
