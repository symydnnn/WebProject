namespace WebProjeleri2022.Models
{
    public class CommentModel
    {

        public int id { get; set; }

        public int photoId  { get; set; }
        
        public KullaniciModel KullaniciModel { get; set; }
        
        public string comment { get; set; }
        public string dateTime { get; set; }

    }
}
