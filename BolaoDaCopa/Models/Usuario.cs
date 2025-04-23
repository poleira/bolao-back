namespace BolaoDaCopa.Models
{
    public class Usuario
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string FirebaseUid { get; protected set; }
        public virtual string Logo { get; protected set; }
        public virtual IEnumerable<Bolao> Boloes { get; protected set; }
        protected Usuario() { }

        public Usuario(string nome, string email, string firebaseUid)
        {
            SetNome(nome);
            SetEmail(email);
            SetFirebaseUid(firebaseUid);
        }

        public virtual void SetNome(string nome)
        {
            Nome = nome;
        }

        public virtual void SetEmail(string email)
        {
            Email = email;
        }

        public virtual void SetFirebaseUid(string firebaseUid)
        {
            FirebaseUid = firebaseUid;
        }
    }
}
