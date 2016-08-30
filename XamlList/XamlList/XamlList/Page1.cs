using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Xamarin.Forms;

namespace XamlList
{
    public class Page1 : ContentPage
    {
        ListView lstView = new ListView();
    

        public Page1()
        {
            Button button = new Button { Text = "Refresh list",
                HorizontalOptions = LayoutOptions.Center };
            button.Clicked += async (object sender, EventArgs e) =>
            {
                await getPost();
            };
            lstView.ItemTemplate = new DataTemplate(typeof(PostCell));
            Content = new StackLayout
            {
                Children = {
                    button,
                    lstView
                }
            };


        }

        private async Task<IEnumerable<Post>> getPost()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("http://small-thunder-6085.getsandbox.com/hello");
            String result = response.Content.ReadAsStringAsync().Result;
            var locationToken = JObject.Parse(result)["posts"];
            Post[] posts = null;
            //Convert 
            posts = locationToken.Select(Post => new Post()
            {
                id = (int)Post["id"],
                title = (string)Post["title"],
                author = (string)Post["author"]

            }).ToArray();
            lstView.ItemsSource = posts;
            return posts;
        }


    }

    public class PostCell : ViewCell
    {
        public const int RowHeight = 55;

        public PostCell()
        {
            var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
            nameLabel.SetBinding(Label.TextProperty, "title");

            var descriptionLabel = new Label { TextColor = Color.Gray };
            descriptionLabel.SetBinding(Label.TextProperty, "author");

            View = new StackLayout
            {
                Spacing = 2,
                Padding = 5,
                Children = {
                nameLabel,
                descriptionLabel,
            },
            };
        }
    }


    public class Post
    {   public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
    }

}
