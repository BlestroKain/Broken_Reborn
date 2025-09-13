using System.Collections.Generic;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game;


public partial class QuestOfferWindow : IQuestWindow
{

    private Button mAcceptButton;

    private Button mDeclineButton;

    private string mQuestOfferText = string.Empty;

    //Controls
    private WindowControl mQuestOfferWindow;

    private ScrollControl mQuestPromptArea;

    private RichLabel mQuestPromptLabel;

    private Label mQuestPromptTemplate;

    private Label mQuestTitle;

    private readonly ScrollControl _rewardItemContainer;

    private readonly ScrollControl _rewardExpContainer;

    public QuestOfferWindow(Canvas gameCanvas)
    {
        mQuestOfferWindow = new WindowControl(gameCanvas, Strings.QuestOffer.Title, false, "QuestOfferWindow");
        mQuestOfferWindow.DisableResizing();
        mQuestOfferWindow.IsClosable = false;

        //Menu Header
        mQuestTitle = new Label(mQuestOfferWindow, "QuestTitle");

        mQuestPromptArea = new ScrollControl(mQuestOfferWindow, "QuestOfferArea");

        mQuestPromptTemplate = new Label(mQuestPromptArea, "QuestOfferTemplate");

        mQuestPromptLabel = new RichLabel(mQuestPromptArea);

        _rewardItemContainer = new ScrollControl(mQuestOfferWindow, "QuestRewardItemContainer");
        _rewardExpContainer = new ScrollControl(mQuestOfferWindow, "QuestRewardExpContainer");

        //Accept Button
        mAcceptButton = new Button(mQuestOfferWindow, "AcceptButton");
        mAcceptButton.SetText(Strings.QuestOffer.Accept);
        mAcceptButton.Clicked += _acceptButton_Clicked;

        //Decline Button
        mDeclineButton = new Button(mQuestOfferWindow, "DeclineButton");
        mDeclineButton.SetText(Strings.QuestOffer.Decline);
        mDeclineButton.Clicked += _declineButton_Clicked;

        mQuestOfferWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        Interface.InputBlockingComponents.Add(mQuestOfferWindow);
    }

    public void AddRewardWidget(Base widget)
    {
        switch (widget)
        {
            case QuestRewardItem:
                widget.Parent = _rewardItemContainer;
                break;
            case QuestRewardExp:
                widget.Parent = _rewardExpContainer;
                break;
            default:
                widget.Parent = _rewardItemContainer;
                break;
        }
    }

    public void ClearRewardWidgets()
    {
        _rewardItemContainer.DeleteAllChildren();
        _rewardExpContainer.DeleteAllChildren();
    }

    private void _declineButton_Clicked(Base sender, MouseButtonState arguments)
    {
        if (Globals.QuestOffers.Count > 0)
        {
            var questId = Globals.QuestOffers[0];
            PacketSender.SendDeclineQuest(questId);
            Globals.QuestOffers.RemoveAt(0);
            Globals.RemoveQuestRewards(questId);
            ClearRewardWidgets();
        }
    }

    private void _acceptButton_Clicked(Base sender, MouseButtonState arguments)
    {
        if (Globals.QuestOffers.Count > 0)
        {
            var questId = Globals.QuestOffers[0];
            PacketSender.SendAcceptQuest(questId);
            Globals.QuestOffers.RemoveAt(0);
            Globals.RemoveQuestRewards(questId);
            ClearRewardWidgets();
        }
    }

    public void Update(QuestDescriptor quest)
    {
        if (quest == null)
        {
            Hide();
            ClearRewardWidgets();
        }
        else
        {
            Show();
            mQuestTitle.Text = quest.Name;
            if (mQuestOfferText != quest.StartDescription)
            {
                mQuestPromptLabel.ClearText();
                mQuestPromptLabel.Width = mQuestPromptArea.Width - mQuestPromptArea.VerticalScrollBar.Width;
                mQuestPromptLabel.AddText(quest.StartDescription, mQuestPromptTemplate);

                mQuestPromptLabel.SizeToChildren(false, true);
                mQuestOfferText = quest.StartDescription;

                ClearRewardWidgets();

                if (Globals.QuestRewards.TryGetValue(quest.Id, out var rewards))
                {
                    foreach (var reward in rewards)
                    {
                        _ = new QuestRewardItem(this, reward.Key, reward.Value);
                    }
                }

                Globals.QuestExperience.TryGetValue(quest.Id, out var playerExp);
                Globals.QuestJobExperience.TryGetValue(quest.Id, out Dictionary<JobType, long>? jobExp);
                Globals.QuestGuildExperience.TryGetValue(quest.Id, out var guildExp);
                Globals.QuestFactionHonor.TryGetValue(quest.Id, out Dictionary<Factions, int>? factionHonor);

                if (playerExp > 0 || (jobExp != null && jobExp.Count > 0) || guildExp > 0 ||
                    (factionHonor != null && factionHonor.Count > 0))
                {
                    _ = new QuestRewardExp(this, playerExp, jobExp, guildExp, factionHonor);
                }
            }
        }
    }

    public void Show()
    {
        mQuestOfferWindow.IsHidden = false;
    }

    public void Close()
    {
        mQuestOfferWindow.Close();
    }

    public bool IsVisible()
    {
        return !mQuestOfferWindow.IsHidden;
    }

    public void Hide()
    {
        mQuestOfferWindow.IsHidden = true;
    }

}
