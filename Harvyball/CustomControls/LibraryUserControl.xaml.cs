using FlatIcons;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Harvyball
{
    /// <summary>
    /// Interaction logic for LibraryUserControl.xaml
    /// </summary>
    

    public partial class LibraryUserControl : UserControl
    {
        //private const string flatIconsKey = "C2EN7ZA0ckcloGzJEdKpaz6YfoqoFOGGELFk4kaOPpacXNXv";
        private const string pexelsKey = "vV1jNw5GWjlYYH6ItvNCNxy8UfmSC3DydyKjingoIVdAlYcjxJ4HUasA";

        // pexels images
        private ObservableCollection<Photo> photos;
        private PexelsClient client;
        private int img_NumberOfPages, img_CurrentPageNumber;

        // flat icons
        private TokenInfo tokenInfo;
        private ObservableCollection<Icon> icons;
        private int ico_NumberOfPages, ico_CurrentPageNumber;
        private readonly PowerPoint.Application thisApplication = Globals.ThisAddIn.Application;
        private readonly Presentation thisPresentation = Globals.ThisAddIn.Application.ActivePresentation;
        private readonly DocumentWindow thisWindow = Globals.ThisAddIn.Application.ActiveWindow;
        public ObservableCollection<BitmapImage> ImagePaths { get; set; }

        public LibraryUserControl()
        {
            try
            {
                InitializeComponent();

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                client = new PexelsClient(pexelsKey);

                photos = new ObservableCollection<Photo>();
                imagesListView.ItemsSource = photos;

                icons = new ObservableCollection<Icon>();
                iconsListView.ItemsSource = icons;

                DataContext = this;
                ImagePaths = new ObservableCollection<BitmapImage>();
                TemplateListView.ItemsSource = ImagePaths;

                this.Loaded += LibraryUserControl_Loaded;
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message); 
            }
        }

        private void LibraryUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
                imagesImg.Source = new BitmapImage(new Uri($"{baseDirPath}\\images\\image.png"));
                iconsImg.Source = new BitmapImage(new Uri($"{baseDirPath}\\images\\icon.png"));
                templatesImg.Source = new BitmapImage(new Uri($"{baseDirPath}\\images\\templates.png"));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void iconsColorsCB_SelectionChanged(object sender, KeyEventArgs e)
        {
            BeginIconsSearch();
            return;
        }
        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (imagesLVI.IsSelected)
                    {
                        BeginImagesSearch();
                        return;
                    }
                    else if (iconsLVI.IsSelected)
                    {
                        BeginIconsSearch();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void categoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (categoryListView.SelectedItem is null)
                    return;

                photos.Clear();
                icons.Clear();
                ImagePaths.Clear();

                inputTextBox.Text = "";
                colorAllCBI.IsSelected = true;
                styleAllCBI.IsSelected = true;
                inputTextBox.Visibility = Visibility.Visible;
                searchButton.Visibility = Visibility.Visible;
                if (imagesLVI.IsSelected)
                {
                    categoriesStackPanel.Visibility = Visibility.Collapsed;
                    imagesListView.Visibility = Visibility.Visible;
                    iconsListView.Visibility = Visibility.Collapsed;
                    TemplateListView.Visibility = Visibility.Collapsed;
                    return;
                }

                if (iconsLVI.IsSelected)
                {
                    categoriesStackPanel.Visibility = Visibility.Visible;
                    iconsCategoriesWrapPanel.Visibility = Visibility.Visible;
                    templatesCategoriesCB.Visibility = Visibility.Collapsed;

                    TemplateListView.Visibility = Visibility.Collapsed;
                    iconsListView.Visibility = Visibility.Visible;
                    return;
                }

                if (templatesLVI.IsSelected)
                {
                    categoriesStackPanel.Visibility = Visibility.Visible;
                    templatesCategoriesCB.Visibility = Visibility.Visible;
                    TemplateListView.Visibility = Visibility.Visible;

                    imagesListView.Visibility = Visibility.Collapsed;
                    iconsCategoriesWrapPanel.Visibility = Visibility.Collapsed;
                    inputTextBox.Visibility = Visibility.Collapsed;
                    searchButton.Visibility = Visibility.Collapsed;
                    iconsListView.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (imagesLVI.IsSelected)
                {
                    BeginImagesSearch();
                    return;
                }
                else if (iconsLVI.IsSelected)
                {
                    BeginIconsSearch();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void imagesListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                ScrollViewer scrollViewer = (ScrollViewer)e.OriginalSource;
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrWhiteSpace(inputTextBox.Text))
                        return;

                    if (img_CurrentPageNumber < img_NumberOfPages)
                    {
                        img_CurrentPageNumber++;

                        PhotoPage page = await client.SearchPhotosAsync(inputTextBox.Text, page: img_CurrentPageNumber);
                        page.photos.ForEach(p => photos.Add(p));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void imagesListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListViewItem listViewItem = (ListViewItem)sender;
                if (listViewItem.DataContext is Photo photo)
                {

                    string url = photo.source.original,
                        extension = "";

                    if (url.EndsWith(".png"))
                        extension = ".png";
                    else if (url.EndsWith(".jpg"))
                        extension = ".jpg";
                    else
                        extension = ".jpeg";

                    using (WebClient client = new WebClient())
                    {
                        var tempFilePath = $"{APIHelper.HarvyballDir.FullName}\\temp{extension}";

                        client.DownloadFile(url, tempFilePath);

                        PowerPoint.Application application = Globals.ThisAddIn.Application;
                        if (application.ActiveWindow.View.Slide is PowerPoint.Slide slide)
                        {
                            PowerPoint.Shape shape = slide.Shapes.AddPicture(tempFilePath,
                                Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                        }


                        try
                        {
                            if (File.Exists(tempFilePath))
                                File.Delete(tempFilePath);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void iconsListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                ScrollViewer scrollViewer = (ScrollViewer)e.OriginalSource;
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrWhiteSpace(inputTextBox.Text))
                        return;

                    if (ico_CurrentPageNumber < ico_NumberOfPages)
                    {
                        ico_CurrentPageNumber++;

                        LoadIcons(ico_CurrentPageNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private  void iconsListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem listViewItem = (ListViewItem)sender;
            if (listViewItem.DataContext is Icon icon)
            {
                string url = icon.Images["512"];
                _ = downloadSVGIcon(url);
            }
        }

        private void TemplateListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {
                string strPPTPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "deckmate", "Templates");
                string strFileName = "";
                if (e.OriginalSource is Image image)
                {
                    strFileName = Path.GetFileName(image.DataContext.ToString());
                }

                string strPPTFileName = strFileName.Substring(0, strFileName.IndexOf("_"));
                int lastUnderscoreIndex = strFileName.LastIndexOf('_');
                string StrslideNumber = strFileName.Substring(lastUnderscoreIndex + 1);
                StrslideNumber = StrslideNumber.Replace(".png", "").Replace(".PNG", "");
                int SlideNumber = Convert.ToInt32(StrslideNumber);

                if (agendaCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "Agenda", strPPTFileName + ".pptx");
                else if (workshopCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "workshop", strPPTFileName + ".pptx");
                else if (projectManagementCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "project Management", strPPTFileName + ".pptx");
                else if (strategyCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "strategy", strPPTFileName + ".pptx");
                else if (tablesCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "tables", strPPTFileName + ".pptx");
                else if (OthersCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "Other", strPPTFileName + ".pptx");
                else if (SavedTemplatesCBI.IsSelected)
                    strPPTPath = Path.Combine(strPPTPath, "Saved Templates", strPPTFileName + ".pptx");
                

                if (thisApplication.ActiveWindow.View.Slide is Slide targetSlide)
                {
                    thisPresentation.Slides.InsertFromFile(strPPTPath, targetSlide.SlideIndex, SlideNumber, SlideNumber);
                    Slide slide = thisPresentation.Slides[targetSlide.SlideIndex + 1];
                    if (slide != null)
                        slide.Select();

                    foreach (PowerPoint.Shape shape in slide.Shapes)
                    {
                        // Check if the shape has a name (not all shapes may have names)
                        if (!string.IsNullOrEmpty(shape.Name))
                        {
                            string shapeName = shape.Name;
                            if(shapeName.Contains("Title"))
                            {
                                PowerPoint.TextFrame textFrame = shape.TextFrame;
                                textFrame.AutoSize = PpAutoSize.ppAutoSizeShapeToFitText;
                                return;
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static async Task downloadSVGIcon(string args)
        {
            try
            {

                string tokenStr = TokenInfo.Deserialize(File.ReadAllText(APIHelper.TokenFilePath)).Data.Token;
                int lastIndex = args.LastIndexOf('/');
                string IconID = args.Substring(lastIndex + 1);
                int dotIndex = IconID.IndexOf('.');
                if (dotIndex >= 0)
                {
                    IconID = IconID.Remove(dotIndex);
                }

                string url = $"https://api.flaticon.com/v3/item/icon/download/{IconID}/svg";
                using (HttpClient client = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Authorization", $"Bearer {tokenStr}");
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var tempFilePath = $"{APIHelper.HarvyballDir.FullName}\\temp.svg";
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                    using (StreamWriter writer = new StreamWriter(tempFilePath))
                    {
                        writer.Write(json);
                    }
                    if (File.Exists(tempFilePath))
                    { 
                        PowerPoint.Application application = Globals.ThisAddIn.Application;
                        if (application.ActiveWindow.View.Slide is PowerPoint.Slide slide)
                        {
                            PowerPoint.Shape shape = slide.Shapes.AddPicture(tempFilePath,Microsoft.Office.Core.MsoTriState.msoFalse,Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void categoriesGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Grid grid = (Grid)sender;
                if (grid.ActualWidth < 225)
                {
                    iconsColorsCB.Width = 80;
                    iconsStylesCB.Width = 80;
                    templatesCategoriesCB.Width = 80;
                }
                else
                {
                    iconsColorsCB.Width = 200;
                    iconsStylesCB.Width = 200;   
                    templatesCategoriesCB.Width = 200;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BeginImagesSearch()
        {
            try
            {
                this.Dispatcher.Invoke(async () =>
                {
                    if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrWhiteSpace(inputTextBox.Text))
                        return;

                    photos.Clear();

                    PhotoPage page1 = await client.SearchPhotosAsync(inputTextBox.Text);
                    PhotoPage page2 = await client.SearchPhotosAsync(inputTextBox.Text, page: 2);

                    page1.photos.ForEach(p => photos.Add(p));
                    page2.photos.ForEach(p => photos.Add(p));

                    img_NumberOfPages = page1.totalResults / page1.perPage;
                    img_CurrentPageNumber = 2;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void iconsColorsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BeginIconsSearch();
            return;
        }

        public void BeginIconsSearch()
        {
            try
            {
                this.Dispatcher.Invoke(async () =>
                {
                    if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrWhiteSpace(inputTextBox.Text))
                        return;

                    icons.Clear();

                    var keyword = HttpUtility.UrlEncode(inputTextBox.Text);
                    string url = $"https://api.flaticon.com/v3/search/icons?q={keyword}";

                    if (blackCBI.IsSelected)
                        url += "&styleColor=black";
                    else if (colorCBI.IsSelected)
                        url += "&styleColor=color";
                    else if (gradientCBI.IsSelected)
                        url += "&styleColor=gradient";
                    else { }


                    if (outlineCBI.IsSelected)
                        url += "&styleShape=outline";
                    else if (fillCBI.IsSelected)
                        url += "&styleShape=fill";
                    else if (linealColorCBI.IsSelected)
                        url += "&styleShape=lineal-color";
                    else if (handDrawnCBI.IsSelected)
                        url += "&styleShape=hand-drawn";
                    else { }

                    url += "&orderBy=priority";

                    tokenInfo = TokenInfo.Deserialize(File.ReadAllText(APIHelper.TokenFilePath));

                    using (HttpClient client = new HttpClient())
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        request.Headers.Add("Accept", "application/json");
                        request.Headers.Add("Authorization", $"Bearer {tokenInfo.Data.Token}");

                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var searchResults = SearchResults.Deserialize(json);
                        searchResults.Icons.ForEach(i => icons.Add(i));
                        if (searchResults.Icons.Count > 0)
                        {
                            ico_NumberOfPages = Convert.ToInt32(searchResults.Metadata.Total / searchResults.Metadata.Count);
                            ico_CurrentPageNumber = 1;
                        }
                        else
                        {
                            string jsonString = @"
                                {
                                    ""data"": [
                                        {
                                            ""id"": 1665715,
                                            ""description"": ""Search"",
                                            ""colors"": ""C0C0C0"",
                                            ""color"": ""gradient"",
                                            ""shape"": ""outline"",
                                            ""family_id"": 57,
                                            ""family_name"": ""Super Basic Omission"",
                                            ""team_name"": ""Freepik"",
                                            ""added"": 1552396963,
                                            ""pack_id"": 1665677,
                                            ""pack_name"": ""UI-UX Interface"",
                                            ""pack_items"": 50,
                                            ""tags"": ""cancel,no results,clear,magnifying glass,search,not found,ui,erase,cross,remove,delete"",
                                            ""equivalents"": 0,
                                            ""images"": {
                                                ""16"": ""https://cdn-icons-png.flaticon.com/16/1665/1665715.png"",
                                                ""24"": ""https://cdn-icons-png.flaticon.com/24/1665/1665715.png"",
                                                ""32"": ""https://cdn-icons-png.flaticon.com/32/1665/1665715.png"",
                                                ""64"": ""https://cdn-icons-png.flaticon.com/64/1665/1665715.png"",
                                                ""128"": ""https://cdn-icons-png.flaticon.com/128/1665/1665715.png"",
                                                ""256"": ""https://cdn-icons-png.flaticon.com/256/1665/1665715.png"",
                                                ""512"": ""https://cdn-icons-png.flaticon.com/512/1665/1665715.png""
                                            }
                                        },
                                        {
                                            ""id"": 6569246,
                                            ""description"": ""Magnifying glass"",
                                            ""colors"": """",
                                            ""color"": ""gradient"",
                                            ""shape"": ""fill"",
                                            ""family_id"": 343,
                                            ""family_name"": ""Gradient Galaxy"",
                                            ""team_name"": ""Freepik"",
                                            ""added"": 1641823673,
                                            ""pack_id"": 6569227,
                                            ""pack_name"": ""Fake News"",
                                            ""pack_items"": 50,
                                            ""tags"": ""error,not found,page not found,magnifying glass,no results,ui,loupe,miscellaneous,cancel,cross"",
                                            ""equivalents"": 0,
                                            ""images"": {
                                                ""16"": ""https://cdn-icons-png.flaticon.com/16/6569/6569246.png"",
                                                ""24"": ""https://cdn-icons-png.flaticon.com/24/6569/6569246.png"",
                                                ""32"": ""https://cdn-icons-png.flaticon.com/32/6569/6569246.png"",
                                                ""64"": ""https://cdn-icons-png.flaticon.com/64/6569/6569246.png"",
                                                ""128"": ""https://cdn-icons-png.flaticon.com/128/6569/6569246.png"",
                                                ""256"": ""https://cdn-icons-png.flaticon.com/256/6569/6569246.png"",
                                                ""512"": ""https://cdn-icons-png.flaticon.com/512/6569/6569246.png""
                                            }
                                        }
                                    ],
                                    ""metadata"": {
                                        ""page"": 1,
                                        ""count"": 2,
                                        ""total"": 2
                                    }
                                }";
                            var Njson = jsonString;
                            var NsearchResults = SearchResults.Deserialize(Njson);
                            NsearchResults.Icons.ForEach(i => icons.Add(i));
                        }
                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void templatesCategoriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ImagePaths.Clear();
                string SearchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "deckmate", "Templates");
                if (agendaCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "agenda");
                else if (workshopCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "workshop");
                else if (projectManagementCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "Project Management");
                else if (strategyCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "strategy");
                else if (tablesCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "Tables");
                else if (OthersCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "Other");
                else if (SavedTemplatesCBI.IsSelected)
                    SearchPath = Path.Combine(SearchPath, "Saved Templates");

                if (Directory.Exists(SearchPath))
                {
                    ImagePaths.Clear();
                    SearchAndLoadImages(SearchPath);
                    TemplateListView.ItemsSource = ImagePaths;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SearchAndLoadImages(string folderPath)
        {
            try
            {
                // Search for PNG files in the current directory
                string[] pngFiles = Directory.GetFiles(folderPath, "*.png");

                // Add the PNG files as BitmapImage objects to the ImagePaths collection
                
                foreach (string filePath in pngFiles)
                {
                    ImagePaths.Add(LoadImage(filePath));
                }

                // Recursively search in subdirectories
                string[] subdirectories = Directory.GetDirectories(folderPath);
                foreach (string subdirectory in subdirectories)
                {
                    SearchAndLoadImages(subdirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private BitmapImage LoadImage(string imagePath)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        public async void LoadIcons(int page)
        {
            try
            {
                if (string.IsNullOrEmpty(inputTextBox.Text) || string.IsNullOrWhiteSpace(inputTextBox.Text))
                    return;

                var keyword = HttpUtility.UrlEncode(inputTextBox.Text);

                string url = $"https://api.flaticon.com/v3/search/icons?q={keyword}&page={page}";

                if (blackCBI.IsSelected)
                    url += "&styleColor=black";
                else if (colorCBI.IsSelected)
                    url += "&styleColor=color";
                else if (colorCBI.IsSelected)
                    url += "&styleColor=gradient";
                else { }


                if (outlineCBI.IsSelected)
                    url += "&styleShape=outline";
                else if (fillCBI.IsSelected)
                    url += "&styleShape=fill";
                else if (linealColorCBI.IsSelected)
                    url += "&styleShape=lineal-color";
                else if (handDrawnCBI.IsSelected)
                    url += "&styleShape=hand-drawn";
                else { }

                tokenInfo = TokenInfo.Deserialize(File.ReadAllText(APIHelper.TokenFilePath));

                using (HttpClient client = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Authorization", $"Bearer {tokenInfo.Data.Token}");

                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var searchResults = SearchResults.Deserialize(json);
                    searchResults.Icons.ForEach(i => icons.Add(i));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearUI()
        {
            try
            {
                photos.Clear();
                icons.Clear();
                inputTextBox.Text = "";

                categoryListView.SelectedItem = null;

                iconsCategoriesWrapPanel.Visibility = Visibility.Collapsed;
                templatesCategoriesCB.Visibility = Visibility.Collapsed;

                imagesListView.Visibility = Visibility.Visible;
                iconsListView.Visibility = Visibility.Collapsed;

                colorAllCBI.IsSelected = true;
                styleAllCBI.IsSelected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
