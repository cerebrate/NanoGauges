﻿using System;
using UnityEngine;


namespace Nereid
{
   namespace NanoGauges
   {
      public abstract class HorizontalTextGauge : AbstractClosableGauge
      {
         private const int MARGIN_VERTICAL = 15;
         private const int MARGIN_HORIZONTAL = 5;
         private const int FONT_SIZE = 14;
         private const int MAX_CHARS = 13;
         private readonly Texture2D back;
         private readonly Texture2D skin;
         private readonly Rect skinBounds;
         private readonly Rect textBounds;

         private GUIStyle SKIN_TEXT = null; 

         public HorizontalTextGauge(int id, Texture2D skin, Texture2D back)
            : base(id)
         {
            this.skin = skin;
            this.back = back;
            this.skinBounds = new Rect(0, 0, GetWidth(), GetHeight());
            double scale = NanoGauges.configuration.gaugeScaling;
            float margin_vert = (float)(MARGIN_VERTICAL * scale);
            float margin_hori = (float)(MARGIN_HORIZONTAL * scale);
            this.textBounds = new Rect(margin_hori, margin_vert, GetWidth() - 2 * margin_hori, GetHeight() - margin_vert);
            //
            if (back == null) Log.Error("no background for gauge " + id + " defined");
            if (skin == null) Log.Error("no skin for gauge " + id + " defined");
         }

         protected override void OnWindow(int id)
         {
            // init
            if(SKIN_TEXT == null)
            {
               SKIN_TEXT = new GUIStyle(GUI.skin.label);
               SKIN_TEXT.fontSize = (int)(FONT_SIZE * NanoGauges.configuration.gaugeScaling);
            }
            //
            // back
            GUI.DrawTexture(skinBounds, back);
            // text
            String text = GetText();
            if(text==null)
            {
               text = "N/A";
            }
            else
            {
               if (text.Length > MAX_CHARS)
               {
                  text = text.Substring(0, MAX_CHARS);
               }
            }
            GUI.Label(textBounds, text, SKIN_TEXT);
            // skin
            GUI.DrawTexture(skinBounds, skin);
         }

         public override sealed int GetWidth()
         {
            return NanoGauges.configuration.horizontalGaugeWidth;
         }

         public override sealed int GetHeight()
         {
            return NanoGauges.configuration.horizontalGaugeHeight;
         }

         public override void On()
         {
         }

         public override void Off()
         {
         }

         public override bool IsOn()
         {
            return true;
         }

         public override void InLimits()
         {
         }

         public override void OutOfLimits()
         {
         }

         public override bool IsInLimits()
         {
            return true;
         }

         public override void Reset()
         {
         }

         protected abstract String GetText();
      }
   }
}
