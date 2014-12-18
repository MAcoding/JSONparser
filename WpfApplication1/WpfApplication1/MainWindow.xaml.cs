using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.IO;

using System.Windows.Controls;
using Newtonsoft.Json;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event RoutedEventHandler LostFocus;
        public event RoutedEventHandler GotFocus;


        BlogPost blogPost = new BlogPost();
        Project project = new Project();

        public MainWindow()
        {
            InitializeComponent();
            PostTitle.Text = "Title";
        }

        private void newBlogPost(object sender, RoutedEventArgs e)
        {
            HeaderTitle.Text = "New Blog Post";
            fileBrowseButton.Visibility = Visibility.Visible;
            fileInfo.Visibility = Visibility.Visible;

            toggleVisibility("post");

        }

        private void newProject(object sender, RoutedEventArgs e)
        {
            fileBrowseButton.Visibility = Visibility.Visible;
            fileInfo.Visibility = Visibility.Visible;

            HeaderTitle.Text = "New Project";

            toggleVisibility("project");

        }

        private void toggleVisibility(string type)
        {
            if (type == "project")
            {
                Existing.Visibility = Visibility.Hidden;
                fileBrowseButton.Visibility = Visibility.Visible;

                PostTitle.Visibility = Visibility.Hidden;
                PostText.Visibility = Visibility.Hidden;
                PostShortText.Visibility = Visibility.Hidden;
                PostImages.Visibility = Visibility.Hidden;
                AddImage.Visibility = Visibility.Hidden;
                RemoveImage.Visibility = Visibility.Hidden;
                AddPost.Visibility = Visibility.Hidden;

                ProjectName.Visibility = Visibility.Visible;
                ProjectBlogButton.Visibility = Visibility.Visible;
                ProjectType.Visibility = Visibility.Visible;
                ProjectDesc.Visibility = Visibility.Visible;
                WriteTags.Visibility = Visibility.Visible;
                AddTag.Visibility = Visibility.Visible;
                RemoveTag.Visibility = Visibility.Visible;
                ProjectTags.Visibility = Visibility.Visible;
                ProjectImageButton.Visibility = Visibility.Visible;
                ProjectImage.Visibility = Visibility.Visible;
                AddProject.Visibility = Visibility.Visible;
            }
            else if (type == "post")
            {
                Existing.Visibility = Visibility.Hidden;
                fileBrowseButton.Visibility = Visibility.Visible;

                ProjectName.Visibility = Visibility.Hidden;
                ProjectBlogButton.Visibility = Visibility.Hidden;
                ProjectType.Visibility = Visibility.Hidden;
                ProjectTags.Visibility = Visibility.Hidden;
                ProjectDesc.Visibility = Visibility.Hidden;
                WriteTags.Visibility = Visibility.Hidden;
                AddTag.Visibility = Visibility.Hidden;
                RemoveTag.Visibility = Visibility.Hidden;
                ProjectImageButton.Visibility = Visibility.Hidden;
                ProjectImage.Visibility = Visibility.Hidden;
                AddProject.Visibility = Visibility.Hidden;

                PostTitle.Visibility = Visibility.Visible;
                PostText.Visibility = Visibility.Visible;
                PostShortText.Visibility = Visibility.Visible;
                PostImages.Visibility = Visibility.Visible;
                AddImage.Visibility = Visibility.Visible;
                RemoveImage.Visibility = Visibility.Visible;
                AddPost.Visibility = Visibility.Visible;
            }
            else
            {
                Existing.Visibility = Visibility.Visible;
                fileBrowseButton.Visibility = Visibility.Hidden;

                ProjectName.Visibility = Visibility.Hidden;
                ProjectBlogButton.Visibility = Visibility.Hidden;
                ProjectType.Visibility = Visibility.Hidden;
                ProjectTags.Visibility = Visibility.Hidden;
                ProjectDesc.Visibility = Visibility.Hidden;
                WriteTags.Visibility = Visibility.Hidden;
                AddTag.Visibility = Visibility.Hidden;
                RemoveTag.Visibility = Visibility.Hidden;
                ProjectImageButton.Visibility = Visibility.Hidden;
                ProjectImage.Visibility = Visibility.Hidden;
                AddProject.Visibility = Visibility.Hidden;

                PostTitle.Visibility = Visibility.Hidden;
                PostText.Visibility = Visibility.Hidden;
                PostShortText.Visibility = Visibility.Hidden;
                PostImages.Visibility = Visibility.Hidden;
                AddImage.Visibility = Visibility.Hidden;
                RemoveImage.Visibility = Visibility.Hidden;
                AddPost.Visibility = Visibility.Hidden;
            }
        }

        private void addImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filepath = dlg.FileName;
                filepath = filepath.Replace("\\", "/");
                filepath = filepath.Substring(filepath.IndexOf("public/") + 7);
                PostImages.Items.Add(filepath);
            }
        }

        private void removeImage(object sender, RoutedEventArgs e)
        {
            PostImages.Items.Remove(PostImages.SelectedItem);
        }

        private void addTag(object sender, RoutedEventArgs e)
        {
            string tags = WriteTags.Text;
            if (tags != String.Empty && tags != null && tags != "Project tags")
            {
                ProjectTags.Items.Add(tags);
            }
            WriteTags.Text = "";

        }

        private void removeTag(object sender, RoutedEventArgs e)
        {
            ProjectTags.Items.Remove(ProjectTags.SelectedItem);
        }

        private void addProjectImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filepath = dlg.FileName;
                ProjectImage.Text = filepath;
            }
        }

        private void TextBox_LostFocus(object sender, System.EventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            setDefaultText(textbox);
        }

        private void TextBox_GotFocus(object sender, System.EventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            setDefaultText(textbox);
        }

        private void setDefaultText(TextBox textbox)
        {
            if (textbox.Text.ToString() == String.Empty)
            {
                textbox.Text = textbox.Tag.ToString();
            }
            else if (textbox.Text.ToString() == textbox.Tag.ToString())
            {
                textbox.Text = "";
            }
        }

        private List<TextBox> inputValidator(TextBox input, List<TextBox> error)
        {
            if (input.Text == String.Empty || input == null || input.Text.ToString() == input.Tag.ToString())
            {
                error.Add(input);
            }
            return error;
        }

        private void printError(List<TextBox> error)
        {
            foreach (TextBox err in error)
            {
                err.BorderBrush = Brushes.Red;
            }

        }

        private void addPost(object sender, RoutedEventArgs e)
        {
            List<TextBox> error = new List<TextBox>();

            error = inputValidator(PostTitle, error);
            error = inputValidator(PostText, error);
            error = inputValidator(PostShortText, error);

            if (error.Count > 0)
            {
                printError(error);
            }
            else
            {
                blogPost.title = PostTitle.Text;
                blogPost.text = PostText.Text;
                blogPost.shortText = PostShortText.Text;
                blogPost.images = PostImages.Items.Cast<String>().ToList();

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(blogPost);

                try
                {
                    string pathToFile;
                    if (HeaderTitle.Text.ToString() == "Modify")
                    {
                        pathToFile = ModifyExisting.Tag.ToString();
                    }
                    else
                    {
                        pathToFile = fileInfo.Tag.ToString();
                    }

                    var jsonData = System.IO.File.ReadAllText(pathToFile);
                    var blogPostList = JsonConvert.DeserializeObject<List<BlogPost>>(jsonData)
                              ?? new List<BlogPost>();
                    if (HeaderTitle.Text.ToString() == "Modify")
                    {
                        BlogPost modifiedPost = blogPostList[Existing.SelectedIndex];

                        modifiedPost.title = blogPost.title;
                        modifiedPost.text = blogPost.text;
                        modifiedPost.shortText = blogPost.shortText;
                        modifiedPost.images = blogPost.images;
                        Console.WriteLine("MODIFIED");
                        Console.WriteLine(modifiedPost.images.ToString());
                    }
                    else
                    {
                        blogPost.id = blogPostList.Count.ToString();
                        blogPost.date = DateTime.Now.ToString();
                        blogPostList.Add(blogPost);
                    }
                    jsonData = JsonConvert.SerializeObject(blogPostList);
                    System.IO.File.WriteAllText(pathToFile, jsonData);
                    StatusBox.Visibility = Visibility.Visible;
                    StatusBox.Foreground = Brushes.Green;
                    StatusBox.Text = "Post successfully added";
                }
                catch
                {
                    StatusBox.Visibility = Visibility.Visible;
                    StatusBox.Foreground = Brushes.Red;
                    StatusBox.Text = "Error - Could not add post to file";
                }
            }
        }

        private void addProject(object sender, RoutedEventArgs e)
        {
            List<TextBox> error = new List<TextBox>();

            error = inputValidator(ProjectName, error);
            error = inputValidator(ProjectType, error);
            error = inputValidator(ProjectDesc, error);

            if (error.Count > 0)
            {
                printError(error);
            }
            else
            {

                project.name = ProjectName.Text;
                project.blog = blogFileInfo.Text;
                project.type = ProjectType.Text;
                project.tags = ProjectTags.Items.Cast<String>().ToList();
                project.desc = ProjectDesc.Text;
                project.image = ProjectImage.Text;

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(project);

                try
                {
                    string pathToFile;
                    if (HeaderTitle.Text.ToString() == "Modify")
                    {
                        pathToFile = ModifyExisting.Tag.ToString();
                    }
                    else
                    {
                        pathToFile = fileInfo.Tag.ToString();
                    }
                    var jsonData = System.IO.File.ReadAllText(pathToFile);

                    var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                  ?? new List<Project>();
                     if (HeaderTitle.Text.ToString() == "Modify")
                    {
                        Project modifiedProject = projectList[Existing.SelectedIndex];

                        modifiedProject.name = project.name;
                        modifiedProject.blog = project.blog;
                        modifiedProject.type = project.type;
                        modifiedProject.tags = project.tags;
                        modifiedProject.desc = project.desc;
                        modifiedProject.image = project.image;

                        
                    }
                    else
                    {
                        project.id = projectList.Count.ToString();
                        project.date = DateTime.Now.ToString();
                        projectList.Add(project);
                    }
                    jsonData = JsonConvert.SerializeObject(projectList);
                    System.IO.File.WriteAllText(pathToFile, jsonData);
                    StatusBox.Visibility = Visibility.Visible;
                    StatusBox.Foreground = Brushes.Green;
                    StatusBox.Text = "Project successfully added";
                }
                catch
                {
                    StatusBox.Visibility = Visibility.Visible;
                    StatusBox.Foreground = Brushes.Red;
                    StatusBox.Text = "Error - Could not add project to file";
                }
            }
        }

        private void fileBrowse(object sender, System.EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            Button button = (Button)sender;

            if (result == true)
            {
                // Open document 
                string filepath = dlg.FileName;
                string filename = Path.GetFileName(filepath);

                if (button.Name == "fileBrowseButton")
                {

                    fileInfo.Text = filename;
                    fileInfo.Tag = filepath;

                }
                else if (button.Name == "ProjectBlogButton")
                {
                    blogFileInfo.Text = filename;
                    blogFileInfo.Tag = filepath;
                }
            }
        }

        protected void OnPropertyChanged(string content)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(content));
            }
        }

        private void modifyExisting(object sender, RoutedEventArgs e)
        {
            Existing.Items.Clear();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    toggleVisibility("modify");
                    
                    HeaderTitle.Text = "Modify";

                    var jsonData = System.IO.File.ReadAllText(dlg.FileName);
                    ModifyExisting.Tag = dlg.FileName;
                    
                    if (dlg.FileName.Contains("project"))
                    {
                        
                        var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                 

                        foreach (Project project in projectList)
                        {
                            Existing.Items.Add(project);
                        }
                        Existing.Tag = "project";
                        Existing.SelectedIndex = 0;
                    }
                    else
                    {
                        var blogPostList = JsonConvert.DeserializeObject<List<BlogPost>>(jsonData)
                                      ?? new List<BlogPost>();

                        Existing.Items.Clear();
                        foreach (BlogPost post in blogPostList)
                        {
                            Existing.Items.Add(post);
                            Existing.SelectedIndex = 0;
                        }
                        Existing.Tag = "post";
                    }
                    Existing.SelectionChanged += new SelectionChangedEventHandler(Existing_SelectedIndexChanged);

                }
                catch
                {
                    StatusBox.Visibility = Visibility.Visible;
                    StatusBox.Foreground = Brushes.Red;
                    StatusBox.Text = "Error - Could not display contents of the file";
                }

            }
        }

        private void Existing_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.Tag.ToString() == "project")
            {
                Project selectedProject = (Project)comboBox.SelectedItem;
             
                toggleVisibility("project");
                Existing.Visibility = Visibility.Visible;
                fileBrowseButton.Visibility = Visibility.Hidden;

                try
                {
                    ProjectName.Text = selectedProject.name;
                    blogFileInfo.Text = selectedProject.blog;
                    ProjectType.Text = selectedProject.type;
                    ProjectDesc.Text = selectedProject.desc;
                    ProjectImage.Text = selectedProject.image;
                    ProjectTags.Items.Clear();
                    foreach (string tag in selectedProject.tags)
                    {
                        ProjectTags.Items.Add(tag);
                    }
                }
                catch
                {
                
                }

            }
            else
            {
                
                try
                {
                    BlogPost selectedPost = (BlogPost)comboBox.SelectedItem;
                    toggleVisibility("post");
                    Existing.Visibility = Visibility.Visible;
                    fileBrowseButton.Visibility = Visibility.Hidden;

                    PostTitle.Text = selectedPost.title;
                    PostText.Text = selectedPost.text;
                    PostShortText.Text = selectedPost.shortText;
                    PostImages.Items.Clear();
                    foreach (string image in selectedPost.images)
                    {
                        PostImages.Items.Add(image);
                    }

                }
                catch
                {

                }
            }
            
        }
    }
}
