using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using Microsoft.Xna.Framework.Audio;
using myEngine;

namespace zCurrentProject
{
    public enum InGamePlayerPosition
    {
        Left,
        Right
    }

    public abstract class Player : GameObject
    {
        //FIELDS
        public Raquette raquette;
        public float speed = 500;

        public Transform anchorPoint;
        public float anchorOffSetX = 80;
        public int ballDirection = 1;

        public Collider2D collider;

        public string name = "Player_Default_Name";
        public PlayerIndex playerIndex;

        //SCORE
        public int score = 0;
        private string scoreKey = "PlayerScore";
        public int lives = 3;

        //BALL
        public bool isHoldingTheBall = false;

        //SOUNDS
        SoundEffect[] target_hit_sounds;

        //CONSTRUCTOR 
        public Player()
        {
            raquette = new Raquette();
            anchorPoint = new Transform();

            anchorPoint.position = new Vector2(raquette.transform.position.X + anchorOffSetX, raquette.transform.position.Y);

            AddComponent(new BoxCollider(raquette.sprite));

            target_hit_sounds = new SoundEffect[] { 
                Ressources.Load<SoundEffect>("myContent/Audio/TargetHit_Small"),
                Ressources.Load<SoundEffect>("myContent/Audio/TargetHit_SmallV2"),
                Ressources.Load<SoundEffect>("myContent/Audio/TargetHit_Medium"),
                Ressources.Load<SoundEffect>("myContent/Audio/TargetHit_Big")
            };
        }

        //METHODS
        public void SetInGamePlayerPosition(InGamePlayerPosition position)
        {
            if (position == InGamePlayerPosition.Left)
            {
                name = "PLAYER A GAUCHE";
                anchorOffSetX = 80;
                ballDirection = 1;

                anchorPoint.position = new Vector2(raquette.transform.position.X + anchorOffSetX, raquette.transform.position.Y);
            }
            else if (position == InGamePlayerPosition.Right)
            {
                name = "PLAYER A DROITE";
                anchorOffSetX = -80;
                ballDirection = -1;

                anchorPoint.position = new Vector2(raquette.transform.position.X + anchorOffSetX, raquette.transform.position.Y);
            }
        }

        public void LoadScore()
        {
            if (SaveSystem_RunTime.data.ContainsKey((scoreKey + playerIndex).ToString()))
                score = Int32.Parse(SaveSystem_RunTime.data[(scoreKey + playerIndex).ToString()]);
        }

        public void SaveScore()
        {
            SaveSystem_RunTime.data.Remove((scoreKey+playerIndex).ToString());
            SaveSystem_RunTime.data.Add((scoreKey+playerIndex).ToString(), score.ToString());
        }


        //UPDATE & DRAW
        public override void Update()
        {
            anchorPoint.position = new Vector2(raquette.transform.position.X + anchorOffSetX, raquette.transform.position.Y);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
        }

        public override void OnCollision(Collider2D other)
        {
        }

        public void FireBall(int direction)
        {
            Scene_Pong.game.ball.FireBall(direction);
            Scene_Pong.game.targetSpawner.Start();
        }

        public void RemoveLife()
        {
            lives--;

            if (lives == 0)
                OnPlayerDie();
        }

        public void OnPlayerGetRemovedStock()
        {
            //AUDIO
            int i = 0;
            if (lives == 3)
                i = 0;
            else if (lives == 2)
                i = 2;
            else if (lives == 1)
                i = 3;
            else if (lives <= 0)
                i = 3;

            AudioSource.PlaySoundEffect(target_hit_sounds[i]);

            //REMOVE LIFE
            RemoveLife();

            //UPDATE UI
            ((UI_Pong)Scene_Pong.ui).RemoveLife(this);

        }

        private Player GetAdversaire()
        {
            Player adversaire = Scene_Pong.game.player1;
            if (adversaire.playerIndex == this.playerIndex)
                adversaire = Scene_Pong.game.player2;

            return adversaire;
        }

        private void OnPlayerDie()
        {
            Particle p = new Particle(DrawSimpleShape.GetTexture(10, 10));
            p.Color = Color.White;
            p.Speed = 10;
            p.TTL = 0.5f;

            ParticleProfile pp = new ParticleProfile(p);
            pp.burstMode = true;
            pp.burstAmount = 150;

            ParticleEngine pe = new ParticleEngine(pp, raquette.sprite.transform.position);
            raquette.sprite.Destroy();

            Settings.GAME_SPEED = 0.2f;

            Scene_Pong.game.OnGameOver(playerIndex);
        }
    }
}