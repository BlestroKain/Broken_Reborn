using System;
using Intersect.Admin.Actions;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Interface;
using Intersect.Client.Interface.Game;
using Intersect.Client.Maps;
using Intersect.Client.Networking;
using Intersect.Utilities;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.MonoGame.Input;
using Intersect.Logging;

namespace Intersect.Client.Core
{
    public static partial class Input
    {

        public delegate void HandleKeyEvent(Keys modifier, Keys key);

        public static HandleKeyEvent KeyDown;

        public static HandleKeyEvent KeyUp;

        public static HandleKeyEvent MouseDown;

        public static HandleKeyEvent MouseUp;

        public static void OnKeyPressed(Keys modifier, Keys key)
        {
            if (key == Keys.None)
            {
                return;
            }

            var consumeKey = false;
            bool canFocusChat = true;

            KeyDown?.Invoke(modifier, key);
            switch (key)
            {
                //by rodrigo. flags the CTRL key
                case Keys.LControlKey:
                    if (Globals.GameState != GameStates.Intro)
                    {
                        Globals.IsControlKeyPressed = true;
                        break;
                    }

                    break;
                //end
                case Keys.Escape:
                    if (Globals.GameState != GameStates.Intro)
                    {
                        break;
                    }

                    Fade.FadeIn();
                    Globals.GameState = GameStates.Menu;

                    return;

                case Keys.Enter:

                    for (int i = Interface.Interface.InputBlockingElements.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            var iBox = (InputBox)Interface.Interface.InputBlockingElements[i];
                            if (iBox != null && !iBox.IsHidden)
                            {
                                iBox.okayBtn_Clicked(null, null);
                                canFocusChat = false;

                                break;
                            }
                        }
                        catch { }

                        try
                        {
                            var eventWindow = (EventWindow)Interface.Interface.InputBlockingElements[i];
                            if (eventWindow != null && !eventWindow.IsHidden && Globals.EventDialogs.Count > 0)
                            {
                                eventWindow.EventResponse1_Clicked(null, null);
                                canFocusChat = false;

                                break;
                            }
                        }
                        catch { }
                    }

                    break;
            }

            if (Controls.Controls.ControlHasKey(Control.OpenMenu, modifier, key))
            {
                if (Globals.GameState != GameStates.InGame)
                {
                    return;
                }

                // First try and unfocus chat then close all UI elements, then untarget our target.. and THEN open the escape menu.
                // Most games do this, why not this?
                if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.ChatFocussed)
                {
                    Interface.Interface.GameUi.UnfocusChat = true;
                }
                else if (Interface.Interface.GameUi != null && Interface.Interface.GameUi.CloseAllWindows())
                {
                    // We've closed our windows, don't do anything else. :)
                }
                else if (Globals.Me != null && Globals.Me.TargetIndex != Guid.Empty)
                {
                    Globals.Me.ClearTarget();
                }
                else
                {
                    Interface.Interface.GameUi?.EscapeMenu?.ToggleHidden();
                }
            }

            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            Controls.Controls.GetControlsFor(modifier, key)
                ?.ForEach(
                    control =>
                    {
                        if (consumeKey)
                        {
                            return;
                        }

                        if (IsModifier(key))
                        {
                            return;
                        }

                        switch (control)
                        {
                            case Control.Screenshot:
                                Graphics.Renderer?.RequestScreenshot();

                                break;

                            case Control.ToggleGui:
                                if (Globals.GameState == GameStates.InGame)
                                {
                                    Interface.Interface.HideUi = !Interface.Interface.HideUi;
                                }

                                break;

                            case Control.OpenDebugger:
                                MutableInterface.ToggleDebug();
                                break;
                        }

                        switch (Globals.GameState)
                        {
                            case GameStates.Intro:
                                break;

                            case GameStates.Menu:
                                break;

                            case GameStates.InGame:
                                switch (control)
                                {
                                    case Control.MoveUp:
                                        break;

                                    case Control.MoveLeft:
                                        break;

                                    case Control.MoveDown:
                                        break;

                                    case Control.MoveRight:
                                        break;

                                    case Control.AttackInteract:
                                        break;

                                    case Control.Block:
                                        Globals.Me?.TryBlock();

                                        break;

                                    case Control.AutoTarget:
                                        Globals.Me?.AutoTarget();

                                        break;

                                    case Control.PickUp:
                                        Globals.Me?.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X);

                                        break;

                                    case Control.Enter:
                                        if (canFocusChat)
                                        {
                                            Interface.Interface.GameUi.FocusChat = true;
                                            consumeKey = true;
                                        }

                                        return;

                                    case Control.Hotkey1:
                                    case Control.Hotkey2:
                                    case Control.Hotkey3:
                                    case Control.Hotkey4:
                                    case Control.Hotkey5:
                                    case Control.Hotkey6:
                                    case Control.Hotkey7:
                                    case Control.Hotkey8:
                                    case Control.Hotkey9:
                                    case Control.Hotkey0:
                                        break;

                                    case Control.OpenInventory:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleInventoryWindow();

                                        break;

                                    case Control.OpenQuests:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleQuestsWindow();

                                        break;

                                    case Control.OpenCharacterInfo:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleCharacterWindow();

                                        break;

                                    case Control.OpenParties:
                                        Interface.Interface.GameUi?.GameMenu?.TogglePartyWindow();

                                        break;

                                    case Control.OpenSpells:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleSpellsWindow();

                                        break;

                                    case Control.OpenFriends:
                                        Interface.Interface.GameUi?.GameMenu?.ToggleFriendsWindow();

                                        break;

                                    case Control.OpenSettings:
                                        Interface.Interface.GameUi?.EscapeMenu?.OpenSettingsWindow();

                                        break;

                                    case Control.OpenAdminPanel:
                                        PacketSender.SendOpenAdminWindow();

                                        break;

                                    case Control.OpenGuild:
                                        Interface.Interface.GameUi?.GameMenu.ToggleGuildWindow();

                                        break;

                                    case Control.TargetParty1:
                                        Globals.Me.TargetPartyMember(0);

                                        break;
                                    case Control.TargetParty2:
                                        Globals.Me.TargetPartyMember(1);

                                        break;
                                    case Control.TargetParty3:
                                        Globals.Me.TargetPartyMember(2);

                                        break;
                                    case Control.TargetParty4:
                                        Globals.Me.TargetPartyMember(4);

                                        break;
                                }

                                break;

                            case GameStates.Loading:
                                break;

                            case GameStates.Error:
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(
                                    nameof(Globals.GameState), Globals.GameState, null
                                );
                        }
                    }
                );
        }

        public static void OnKeyReleased(Keys modifier, Keys key)
        {
            KeyUp?.Invoke(modifier, key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }
            
            if(key == Keys.LControlKey)
            {
                Globals.IsControlKeyPressed = false;
            }
        }

        public static void OnMouseDown(Keys modifier, MouseButtons btn)
        {
            var key = Keys.None;
            switch (btn)
            {
                case MouseButtons.Left:
                    key = Keys.LButton;
                                 //editado por rodrigo

                    if 
                        (Globals.GameState == GameStates.InGame && 
                        Globals.Me != null && 
                        Interface.Interface.HasInputFocus() == false && 
                        Interface.Interface.MouseHitGui() == false &&
                        Globals.Me.TryTarget() == false)
                    {
                        Pointf mouse;
                        bool move_it = false;
                        //get the mouse position
                        mouse = Globals.InputManager.GetMousePosition();

                        Globals.MouseClick.click_image_file = "click_marker_left.png";
                        Globals.MouseClick.is_click_image_on = true;

                        Pointf getpoint = Globals.Me.GetClickedTileDistance(mouse);
                        var dif_x = (int)getpoint.X;
                        var dif_y = (int)getpoint.Y;
                        //ok, now we convert to positive integer to check the biggest difference, horizontal or vertical so we can decide where to move
                        var chk_x = 0; var chk_y = 0; var direction = "";

                        if (dif_x < 0) 
                        { 
                            chk_x = (dif_x * (-1)); 
                        } else {
                            chk_x = dif_x;
                        }
                        if (dif_y < 0) 
                        { 
                            chk_y = (dif_y * (-1)); 
                        } else { 
                            chk_y = dif_y; 
                        }

                        //we clear any existing movement before setting a new one
                        //Globals.Me.multi_mouse_move_active = false;
                        Globals.Me.multi_mouse_move_count = -1;

                        if(chk_x > chk_y)
                        { //horizontal movement
                            Globals.Me.multi_mouse_move_count = chk_x;
                            if (dif_x  < 0)
                            { // move left
                                direction = "left";
                                Globals.Me.multi_mouse_move_direction = 1;
                                move_it = true;
                            }
                            else
                            { // move right
                                direction = "right";
                                Globals.Me.multi_mouse_move_direction = 3;
                                move_it = true;
                            }

                        }
                        if (chk_x < chk_y)
                        { //vertical movement
                            Globals.Me.multi_mouse_move_count = chk_y;
                            if (dif_y < 0)
                            { // move up
                                direction = "up";
                                Globals.Me.multi_mouse_move_direction = 0;
                                move_it = true;
                            }
                            else
                            { // move down
                                direction = "down";
                                Globals.Me.multi_mouse_move_direction = 2;
                                move_it = true;
                            }
                        }
                        //for maximize performance, if steps are above X value, increase X steps
                        switch (Globals.Me.multi_mouse_move_count)
                            {
                                case int n when (n >= 5 && n < 10):
                                Globals.Me.multi_mouse_move_count = (int)(Math.Floor(Globals.Me.multi_mouse_move_count * 1.5));
                                break;
                            case int n when (n >= 10 && n < 100):
                                Globals.Me.multi_mouse_move_count = (int)(Math.Floor(Globals.Me.multi_mouse_move_count * 2.5));
                                break;

                        }
                        //Globals.Me.multi_mouse_move_count = 20;

                        //tudo que vier abaixo será uma função pra achar o tile real no mundo.

                        var myMap = MapInstance.Get(Globals.Me.MapId);
                        
                        
                        if (myMap != null)
                        {

                            int mapgx = (int)MapInstance.Get(Globals.Me.MapId).GridX;
                            int mapgy = (int)MapInstance.Get(Globals.Me.MapId).GridY;

                            int targetx = mapgx; int targety = mapgy;

                            //verifica se o player está em cima da última tile do mapa no sentido x
                            if (Globals.Me.X + dif_x > Options.MapWidth)
                             {
                            //PacketSender.SendChatMsg($"Ultrapassa mapa para direita", 0);
                                targetx = mapgx + 1;
                            }
                            if (Globals.Me.X + dif_x < 0)
                            {
                                targetx = mapgx - 1;
                                //PacketSender.SendChatMsg($"Ultrapassa mapa para esquerda", 0);
                            }

                            if (Globals.Me.Y + dif_y > Options.MapHeight)
                            {
                                //PacketSender.SendChatMsg($"Ultrapassa mapa para baixo", 0);
                                targety = mapgy + 1;
                            }
                            if (Globals.Me.Y + dif_y < 0)
                            {
                                targety = mapgx - 1;
                                //PacketSender.SendChatMsg($"Ultrapassa mapa para cima", 0);
                            }

                            int mouse_x = (int)(mouse.X / Options.TileWidth);
                            int mouse_y = (int)(mouse.Y / Options.TileHeight);
                            //make sure the values are within the grid limits
                            
                            if (targetx < Globals.MapGrid.GetLength(0) && targety < Globals.MapGrid.GetLength(1) && targetx > -1 && targety > -1)
                            {
                                try
                                {
                                    var targetMap = MapInstance.Get(Globals.MapGrid[targetx, targety]);
                                } catch
                                {
                                    Log.Error("fudeu");
                                }
                            }

                            //Calculate World Tile of Me
                            var x1 = Globals.Me.X + myMap.GridX * Options.MapWidth;
                            var y1 = Globals.Me.Y + myMap.GridY * Options.MapHeight;

                            //Calculate world tile of target
                            //var x2 = target.X + targetMap.MapGridX * Options.MapWidth;
                            //var y2 = target.Y + targetMap.MapGridY * Options.MapHeight;

                            //return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

                            string[] anim_list = GameObjects.AnimationBase.GetNameList();
                            
                            
                            for(int i = 0; i <= anim_list.Length -1; i++)
                            {
                                Guid anim_id = GameObjects.AnimationBase.IdFromList(i);
                                string ani_name = GameObjects.AnimationBase.GetName(anim_id);
                                if (ani_name == "mouse_click_animation")
                                {
                                    // PacketSender.SendChatMsg("animation found", 0);

                                    //targetMap.AddMouseClickAnimation(anim_id, mouse_x, mouse_y);
                                    //Graphics.DrawGameTexture

                                }
                                
                            }
                            //targetMap
                        }

                        if (move_it == true && Globals.IsControlKeyPressed == false)
                        {
                            Globals.Me.IsMoving = true;
                            Globals.Me.HandleInput(Globals.Me.multi_mouse_move_direction);
                            Globals.Me.multi_mouse_move_active = true;

                            //The commented code below shows a chat bubble with the direction and "steps". For testing purposes.
                            PacketSender.SendChatMsg("going " + direction + " " + Globals.Me.multi_mouse_move_count.ToString() + " steps." , 0);
                            //PacketSender.SendChatMsg($"player:  {player_x},{player_y}  mouse: {mouse_x}, {mouse_y} real mouse: {real_mouse_x} pl-px: {real_pl_x} Steps to walk: {steps_to_walk} " , 0);
                            //PacketSender.SendChatMsg($"diferenca x: {dif_x} diferenca y: {dif_y}", 0);
                            //PacketSender.SendChatMsg($"map len:{Globals.MapGrid.Length}",0);
                        }
                    }
                    //fim do editado por rodrigo
                    break;

                case MouseButtons.Right:
                    key = Keys.RButton;        
                    
                    Globals.MouseClick.click_image_file = "click_marker_right.png";
                    Globals.MouseClick.is_click_image_on = true;

                    break;

                case MouseButtons.Middle:
                    key = Keys.MButton;

                    break;
            }

            MouseDown?.Invoke(modifier, key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.GameState != GameStates.InGame || Globals.Me == null)
            {
                return;
            }

            if (Interface.Interface.MouseHitGui())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (Globals.Me.TryTarget())
            {
                return;
            }

            if (Controls.Controls.ControlHasKey(Control.PickUp, modifier, key))
            {
                if (Globals.Me.TryPickupItem(Globals.Me.MapInstance.Id, Globals.Me.Y * Options.MapWidth + Globals.Me.X, Guid.Empty, true))
                {
                    return;
                }

                if (!Globals.Me.IsAttacking)
                {
                    Globals.Me.AttackTimer = Timing.Global.Milliseconds + Globals.Me.CalculateAttackTime();
                }
            }

            if (Controls.Controls.ControlHasKey(Control.Block, modifier, key))
            {
                if (Globals.Me.TryBlock())
                {
                    return;
                }
            }

            if (key != Keys.None)
            {
                OnKeyPressed(modifier, key);
            }
        }

        public static void OnMouseUp(Keys modifier, MouseButtons btn)
        {
            var key = Keys.LButton;
            switch (btn)
            {
                case MouseButtons.Right:
                    key = Keys.RButton;
                    //by rodrigo
                    if
                        (Globals.GameState == GameStates.InGame &&
                         Globals.Me != null &&
                         Interface.Interface.HasInputFocus() == false &&
                         Interface.Interface.MouseHitGui() == false &&
                         Globals.Me.TryTarget() == false && Globals.Me.IsMoving == false)
                    {
                        Pointf mouse;
                        //send the player direction to the server again
                        PacketSender.SendDirection(Globals.Me.Dir);
                        //get the mouse position
                        mouse = Graphics.ConvertToWorldPoint(Globals.InputManager.GetMousePosition());
                        Globals.Me.LongRangeSpellTarget.X = (int)mouse.X / Options.TileWidth;
                        Globals.Me.LongRangeSpellTarget.Y = (int)mouse.Y / Options.TileHeight;
                        Globals.Me.PlayerView.X = (int)Graphics.CurrentView.Left;
                        Globals.Me.PlayerView.Y = (int)Graphics.CurrentView.Top;



                        Globals.Me.start_hotbar_selected_spell = true;                    
                    }
//end

                    break;

                case MouseButtons.Middle:
                    key = Keys.MButton;

                    break;
            }

            MouseUp?.Invoke(modifier, key);
            if (Interface.Interface.HasInputFocus())
            {
                return;
            }

            if (Globals.Me == null)
            {
                return;
            }

            if (btn != MouseButtons.Right)
            {
                return;
            }

            if (Globals.InputManager.KeyDown(Keys.Shift) != true)
            {
                return;
            }

            var x = (int) Math.Floor(Globals.InputManager.GetMousePosition().X + Graphics.CurrentView.Left);
            var y = (int) Math.Floor(Globals.InputManager.GetMousePosition().Y + Graphics.CurrentView.Top);

            foreach (MapInstance map in MapInstance.Lookup.Values)
            {
                if (!(x >= map.GetX()) || !(x <= map.GetX() + Options.MapWidth * Options.TileWidth))
                {
                    continue;
                }

                if (!(y >= map.GetY()) || !(y <= map.GetY() + Options.MapHeight * Options.TileHeight))
                {
                    continue;
                }

                //Remove the offsets to just be dealing with pixels within the map selected
                x -= (int) map.GetX();
                y -= (int) map.GetY();

                //transform pixel format to tile format
                x /= Options.TileWidth;
                y /= Options.TileHeight;
                var mapNum = map.Id;

                if (Globals.Me.TryGetRealLocation(ref x, ref y, ref mapNum))
                {
                    PacketSender.SendAdminAction(new WarpToLocationAction(map.Id, (byte) x, (byte) y));
                }

                return;
            }
        }

        public static bool IsModifier(Keys key)
        {
            switch (key)
            {
                case Keys.Control:
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.Shift:
                case Keys.ShiftKey:
                case Keys.Alt:
                    return true;

                default:
                    return false;
            }
        }
    }

}
