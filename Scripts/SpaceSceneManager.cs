using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace t3
{
    public partial class SpaceSceneManager : Node2D
    {
        [Export]
        PackedScene spaceChunkScene;

        [Export]
        Node2D chunkMapNode;

        [Export]
        StarUI starInfoUI;

        [Export]
        HelpUI helpInfoUI;

        private Dictionary<Vector2I, Node2D> chunkMap = new();
        private int chunkSize = 256;

        private bool holding = false;
        private float holdTimer = 0;
        private float holdThrehold = 0.075f;

        public static SpaceSceneManager instance;

        public override void _Ready()
        {
            instance = this;
            chunkMapNode.Position += Vector2.Right * 4 * 256;
            chunkMapNode.Position += Vector2.Down  * 2 * 256;
        }

        public override void _Process(double delta)
        {
            if (holding) holdTimer += (float)delta;
            else holdTimer = 0;

            Vector2 viewRectSize = GetViewport().GetVisibleRect().Size;
            int chunkRows = (int)(viewRectSize.X / chunkSize) + 4;
            int chunkCols = (int)(viewRectSize.Y / chunkSize) + 4;

            for (int i = chunkMap.Count - 1; i >= 0; --i)
            {
                Vector2I key = chunkMap.Keys.ElementAt(i);
                if (key.X >= Math.Round((-chunkMapNode.Position.X - chunkSize * 2) / chunkSize) &&
                    key.X <= Math.Round((-chunkMapNode.Position.X - chunkSize * 2 + chunkRows * chunkSize) / chunkSize) &&
                    key.Y >= Math.Round((-chunkMapNode.Position.Y - chunkSize * 2) / chunkSize) &&
                    key.Y <= Math.Round((-chunkMapNode.Position.Y - chunkSize * 2 + chunkCols * chunkSize) / chunkSize)
                ) continue;

                chunkMap[key].QueueFree();
                chunkMap.Remove(key);
            }

            Vector2I topLeftChunkCord = new (
                (int)Math.Round(-chunkMapNode.Position.X - chunkSize * 2) / chunkSize,
                (int)Math.Round(-chunkMapNode.Position.Y - chunkSize * 2) / chunkSize
            );

            for (int x = 0; x < chunkRows; x++)
                for (int y = 0; y < chunkCols; y++)
                {
                    Vector2I chunkIdx = topLeftChunkCord + Vector2I.Right * x + Vector2I.Down * y;
                    if (chunkMap.ContainsKey(chunkIdx) == false)
                    {
                        SpaceChunk n = spaceChunkScene.Instantiate<SpaceChunk>();
                        n.Position = chunkIdx * chunkSize;
                        n.SetCoords(chunkIdx.ToString());
                        chunkMap.Add(chunkIdx, n);
                        chunkMapNode.AddChild(n);
                    }
                }
        }

        public override void _Input(InputEvent @event)
        {
            if (IsUIOpen()) return;

            if (@event is InputEventMouseMotion)
            {
                InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
                if (Input.IsMouseButtonPressed(MouseButton.Left))
                {
                    holding = true;
                    if (holdTimer < holdThrehold) return;

                    Input.MouseMode = Input.MouseModeEnum.Captured;
                    chunkMapNode.Position += mouseMotion.Relative;
                }
                else
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                    holding = false;
                }
            } 

            else if (@event is InputEventKey)
            {
                InputEventKey keyEvent = @event as InputEventKey;
                if (keyEvent.Keycode == Key.H) helpInfoUI.Visible = true; 
            }
        }

        public void OpenStarUI(Star star)
        {
            starInfoUI.PopulateStarInfo(star);
            starInfoUI.Visible = true;
        }

        public bool IsUIOpen()
        {
            return starInfoUI.Visible || helpInfoUI.Visible;
        }

    }
}
