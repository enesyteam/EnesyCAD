

namespace Enesy.EnesyCAD.CommandManager
{
    class CommandInfoCollection : System.Collections.CollectionBase
    {
        [System.Runtime.CompilerServices.IndexerName("Commands")]
        private CommandInfo this[int index]
        {
            get
            {
                return this.List[index] as CommandInfo;
            }
            set
            {
                this.List[index] = value;
            }
        }

        public void Add(CommandInfo cmdInfo)
        {
            List.Add(cmdInfo);
        }

        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }
    }
}
