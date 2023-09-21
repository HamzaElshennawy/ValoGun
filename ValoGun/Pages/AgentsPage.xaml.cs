namespace ValoGun.Pages;

public partial class AgentsPage : ContentPage
{
	public AgentsPage()
	{
		InitializeComponent();
		ChangeClipToBounds();
	}


	//make functuin that make the frames inside the collection view dont clip to bounds

	public void ChangeClipToBounds()
	{
		var CollectionView = this.FindByName<CollectionView>("AgentList");
		//get all the frames inside the collection view
        foreach (var item in CollectionView.ItemsSource)
		{
            var frame = item as Frame;
            frame.IsClippedToBounds = false;
        }
    }

}
