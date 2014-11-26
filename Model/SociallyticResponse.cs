namespace Model
{
    public class SociallyticResponse
    {
        public string info { get; set; }
        public int count_of_words { get; set; }
        public string sentiment { get; set; }
        public string text { get; set; }
        public int analysis_duration { get; set; }
        public float sentiment_score { get; set; }
        public float sentiment_score_words { get; set; }
    }
}