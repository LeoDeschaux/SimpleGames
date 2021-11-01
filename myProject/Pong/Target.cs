using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public class Target : GameObject, IDisposable
    {
        //FIELDS
        public Sprite sprite;

        //CONSTRUCTOR
        public Target()
        {
            sprite = new Sprite(new Vector2(Settings.GetScreenCenter().X, Settings.GetScreenCenter().Y), new Vector2(50, 50));
            sprite.color = Color.HotPink;
            sprite.drawOrder = 0;
            sprite.transform.rotation = 45f;
            this.AddComponent(new BoxCollider(sprite));
            //this.GetComponent<BoxCollider>().scale = 2;
        }

        //METHODS

        //UPDATE & DRAW
        public override void Update()
        {
        }

        public override void OnDestroy()
        {
            Particle p = new Particle(DrawSimpleShape.GetTexture(10, 10));
            p.Color = Color.HotPink;
            p.Speed = 10;
            p.TTL = 0.5f;

            ParticleProfile pp = new ParticleProfile(p);
            pp.burstMode = true;
            pp.burstAmount = 50;

            ParticleEngine pe = new ParticleEngine(pp, sprite.transform.position);

            this.GetComponent<BoxCollider>().Destroy();
            sprite.Destroy();

            Scene_Pong.game.targetSpawner.SpawnNewTarget();
        }

        public override void OnCollision(Collider2D other)
        {
        }
    }
}
