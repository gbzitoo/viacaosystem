namespace ViacaoSystemAPI.Entity
{
    public class TodoItem
    {
        public TodoItem()
        {
            this.Id = Guid.NewGuid();
            IsDeleted = false;
        }
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Descricao { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }

        public void updated(string title, string  descricao, bool isCompleted )
        {
            Title = title;
            Descricao = descricao;
            IsCompleted = isCompleted; 
        }

        public void deleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

    }
}