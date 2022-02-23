using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }
        public Slider Slider { get; set; }

        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }

        public About About { get; set; }
        public AboutText AboutText { get; set; }

        public List<Expert> Experts { get; set; }
        public ExpertTitle ExpertTitle { get; set; }

        public BlogTitle BlogTitle { get; set; }

        public List<Blogger> Bloggers { get; set; }

        public List<saySlider> SaySliders { get; set; }

        public SubscribeImg SubscribeImg { get; set; }


        public List<InstagramImg> InstagramImgs { get; set; }
    }
}