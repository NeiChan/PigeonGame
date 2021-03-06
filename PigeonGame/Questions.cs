﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PigeonGame
{
	public class Questions
	{
		private World _world;
		private SpriteFont _font;
		private KeyboardState _keyboard, OldKeyState;
		private bool _correctAnswer1, _correctAnswer2, _correctAnswer3, _correctAnswer4;
		private bool _CorrectAnswer = false, _WrongAnswer = false;

		float _delay = 0;

		public Questions (World w)
		{
			_world = w;
			_font = Assets.Font;
			_correctAnswer1 = false;
			_correctAnswer2 = false;
			_correctAnswer3 = false;
			_correctAnswer4 = false;
		}

		public void Update(GameTime gameTime)
		{
			_keyboard = Keyboard.GetState ();
			switch (Assets.GenerateNumber) {
			case 1:
				_correctAnswer2 = true;
				break;
			case 2:
				_correctAnswer4 = true;
				break;
			case 3:
				_correctAnswer3 = true;
				break;
			case 4:
				_correctAnswer1 = true;
				break;
			case 5:
				_correctAnswer4 = true;
				break;
			case 6:
				_correctAnswer3 = true;
				break;
			case 7:
				_correctAnswer4 = true;
				break;
			case 8:
				_correctAnswer1 = true;
				break;
			case 9:
				_correctAnswer2 = true;
				break;
			case 10:
				_correctAnswer3 = true;
				break;
			}

			switch (Assets.BossGenerateNumber) {
			case 1:
				_correctAnswer4 = true;
				break;
			case 2:
				_correctAnswer1 = true;
				break;
			case 3:
				_correctAnswer1 = true;
				break;
			case 4:
				_correctAnswer4 = true;
				break;
			case 5:
				_correctAnswer3 = true;
				break;
			/*
			case 6:
				break;
			case 7:
				break;
			case 8:
				break;
			case 9:
				break;
			case 10:
				break;
			*/
			}

			KeyboardState NewKeyState = Keyboard.GetState();

			if (_world.PidgyHitEnemy || _world.StartBossLevel) {
				if (NewKeyState.IsKeyDown (Keys.D1) && OldKeyState.IsKeyUp (Keys.D1)) {
					if (_correctAnswer1) {						
						CorrectOrNot(gameTime, "Yes");
						BossLoseLife ();
					} else {
						CorrectOrNot(gameTime, "No");
						LoseLife ();
					}
					_world.PidgyHitEnemy = false;
					_world.InitiateAttack = false;
				}

				if (NewKeyState.IsKeyDown (Keys.D2) && OldKeyState.IsKeyUp (Keys.D2)) {
					if (_correctAnswer2) {
						CorrectOrNot(gameTime, "Yes");
						BossLoseLife ();
					} else {
						CorrectOrNot(gameTime, "No");
						LoseLife ();
					}
					_world.PidgyHitEnemy = false;
					_world.InitiateAttack = false;
				}

				if (NewKeyState.IsKeyDown (Keys.D3) && OldKeyState.IsKeyUp (Keys.D3)) {
					if (_correctAnswer3) {
						CorrectOrNot(gameTime, "Yes");
						BossLoseLife ();
					} else {
						CorrectOrNot(gameTime, "No");
						LoseLife ();
					}
					_world.PidgyHitEnemy = false;
					_world.InitiateAttack = false;
				}

				if (NewKeyState.IsKeyDown (Keys.D4) && OldKeyState.IsKeyUp (Keys.D4)) {
					if (_correctAnswer4) {
						CorrectOrNot(gameTime, "Yes");
						BossLoseLife ();
					} else {
						CorrectOrNot(gameTime, "No");
						LoseLife ();
					}
					_world.PidgyHitEnemy = false;
					_world.InitiateAttack = false;
				}

				OldKeyState = NewKeyState;
			}

			if (gameTime.TotalGameTime >= Assets.IntervalFromQuestion) {
				Assets.QuestionGivenWaiting = false;
				_CorrectAnswer = false;
				_WrongAnswer = false;
				_correctAnswer1 = false;
				_correctAnswer2 = false;
				_correctAnswer3 = false;
				_correctAnswer4 = false;
			}
				
			/*
			Console.WriteLine ("CORRECT ANSWER BOOLEAN " + _CorrectAnswer);
			Console.WriteLine ("WRONG ANSWER BOOLEAN " + _WrongAnswer);
			Console.WriteLine ("ANSWER A " + _correctAnswer1);
			Console.WriteLine ("ANSWER B " + _correctAnswer2);
			Console.WriteLine ("ANSWER C " + _correctAnswer3);
			Console.WriteLine ("ANSWER D " + _correctAnswer4);
			*/

			//Console.WriteLine ("LevelState = " + _world.LevelState);
			//Console.WriteLine ("TotalLife = " + _world.TotalLife);
		}

		public void LoseLife()
		{
			if (_world.LevelState != 1) {
				_world.TotalLife--;
			}
		}

		public void BossLoseLife()
		{
			if (_world.LevelState == 5) {
				_world.BossTotalLife--;
			}
		}

		public void CorrectOrNot(GameTime gameTime, string checkCorrect)
		{
			switch(checkCorrect) {
			case "Yes":
				_CorrectAnswer = true;
				break;
			case "No":
				_WrongAnswer = true;
				break;
			}

			Assets.IntervalFromQuestion = gameTime.TotalGameTime + TimeSpan.FromMilliseconds (1000);
			Assets.QuestionGivenWaiting = true;
			if (Assets.QuestionGivenWaiting) 
			{
				_world.paused = false;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Assets.QuestionScreen, new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

			if (Assets.QuestionGivenWaiting) {
				if (_CorrectAnswer) {
					spriteBatch.DrawString (_font, "Goedzo! Je hebt het juiste antwoord gegeven.", new Vector2 (50, 350), Color.LightGreen);
				} else if (_WrongAnswer) {
					spriteBatch.DrawString (_font, "Jammer! Je antwoord was niet juist, je verliest een leven.", new Vector2 (50, 350), Color.OrangeRed);
				}
			}

			switch (Assets.GenerateNumber) {
			case 1:
				spriteBatch.DrawString (_font, 
					"Het is 8 uur 's ochtends, tijd om op te staan. Kraaltje en Knoopje zijn al een tijdje \n" +
					"wakker. Hun favoriete ochtendshow is op televisie. Pareltje haast zich snel \n" +
					"naar de keuken om wormenpap te maken. De kinderen hebben namelijk nog een \n" +
					"half uur voordat school begint. Na een aantal keren te hebben gesluimerd \n" +
					"staat ook Pidgey eindelijk op. Hij poetst snel zijn snavel, eet zijn ontbijt \n" +
					"en vertrekt dan naar zijn werk.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom waren de kinderen al wakker?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Door het getoeter van de melkboer", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Voor hun favoriete ochtendshow", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat mama ze gewekt heeft", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Om huiswerk te maken", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 1");
				break;
			case 2:
				spriteBatch.DrawString (_font, 
					"Lang geleden toen Pidgey nog op de conducteursopleiding zat kwam hij een meisje \n" +
					"genaamd Pareltje tegen. Pareltje zat op de stewardessenopleiding en tijdens een \n" +
					"uitwisseling tussen beide opleidingen moesten Pidgey en Pareltje achterhalen wat \n" +
					"de verschillen zijn tussen een conducteur in een trein en een stewardess in een \n" +
					"vliegtuig. Tijdens dit gesprek kwamen de twee duiven er achter dat zij niet veel \n" +
					"van elkaar verschillen maar juist heel veel gemeen hebben. Na een aantal keer te \n" +
					"hebben afgesproken besloten de twee duifjes te trouwen.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom vond er een uitwisseling tussen beide opleidingen plaats?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Om elkaar beter te leren kennen", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Om met elkaar af te spreken", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Om te achterhalen wat Pareltje en Pidgey gemeen hebben", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Om zo de verschillen tussen beide beroepen te achterhalen", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 2");
				break;
			case 3:
				spriteBatch.DrawString (_font, 
					"Pidgey is een hardwerkende conducteur die elke dag naar zijn werk vliegt. \n" +
					"Dit lukt niet altijd, helaas zijn er veel dieren ontsnapt uit het \n" +
					"dierentuin die Pidgey moet ontwijken tijdens zijn reis naar het station. \n" +
					"Op het nieuws hoort Pidgey dat er arenden rondvliegen op de route naar het \n" +
					"station. Deze zijn gemeen en lusten wel een duifje.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Hoe weet Pidgey dat er arenden rond vliegen op de route naar het station?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Dat heeft Pareltje hem verteld", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Er zijn dieren ontsnapt uit het dierentuin", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Door het nieuws", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Omdat er altijd arenden vliegen op de route naar het station", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 3");
				break;
			case 4:
				spriteBatch.DrawString (_font, 
					"Nadat Pidgey en Pareltje getrouwd waren hebben zij een meisje gekregen. \n" +
					"Dit meisje werd Kraaltje genoemd. Twee jaar later kregen zij er nog een \n" +
					"jongetje bij, deze noemden zij Knoopje. Knoopje zit nog in de peuterklas \n" +
					"terwijl Kraaltje al in de kleuterklas zit.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Hoeveel jaar is Kraaltje ouder dan Knoopje?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. 2 jaar", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. 3 jaar", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. 4 jaar", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. even oud", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 4");
				break;
			case 5:
				spriteBatch.DrawString (_font, 
					"Nadat Pidgey thuis is aangekomen merkt Pidgey iets merkwaardigs op. \n" +
					"Zijn gezin is nergens te bekennen. Hij vindt een briefje waarop staat \n" +
					"dat zijn gezin is ontvoerd door de draak, de boze draak die al jaren \n" +
					"zijn beruchte vijand is. Pidgey is erg bezorgd en wil kosten wat het \n" +
					"kost zijn familie bevrijden uit de klauwen van de draak. De draak die \n" +
					"ooit een goede vriend van Pidgey was wil nu wraak nemen.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom heeft de draak Pidgey's gezin ontvoerd?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Dat is niet duidelijk", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Omdat de draak Pidgey's vijand is", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat Pidgey niet meer de vriend van de draak is", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. De draak wil wraak nemen", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 5");
				break;
			case 6:
				spriteBatch.DrawString (_font, 
					"De draak en Pidgey hadden 15 jaar geleden veel gemeen zo veel gemeen \n" +
					"dat ze beste vrienden waren, ze hielden beiden van treinen de draak \n" +
					"droomde er altijd van om zelf ooit een trein te mogen besturen. Helaas \n" +
					"was dit niet mogelijk omdat de draak moeite had met zien, toen Pidgey \n" +
					"eindelijk een treinbestuurder werd was de draak woedend en besloot wraak \n" +
					"te nemen.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Hoe oud is de draak?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. 45", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. 32", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Dat is niet duidelijk", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. 15", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 6");
				break;
			case 7:
				spriteBatch.DrawString (_font, 
					"De draak nam Pidgey zijn kinderen en vrouw mee naar zijn kasteel, \n" +
					"in het kasteel bevonden zich erg veel dieren, dit kasteel had ook \n" +
					"een 12 meter hoge toren. Ook had het kasteel 1001 kamers. Pidgey \n" +
					"was vastbesloten om zijn familie terug te halen.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Hoeveel kamers had het kasteel?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. 12", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. 101", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. 10001", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. 1001", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 7");
				break;
			case 8:
				spriteBatch.DrawString (_font, 
					"De zomer begint over een week en de zomervakantie nadert. \n" +
					"Pidgey heeft daarom een vakantie geboekt naar de Maldiven. \n" +
					"Hij heeft van vrienden en collega's gehoord dat het daar erg mooi \n" +
					"schijnt te zijn. Vooral de kust van het eilandje Hulhumale, de \n" +
					"hoofdstad van de Maldiven schijnt prachtig te zijn.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waar schijnt het erg mooi te zijn?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Hulhumale, de hoofdstad van de Maldiven", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. De kust", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Maldiven", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. De hoofdstad van de Maldiven", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 8");
				break;
			case 9:
				spriteBatch.DrawString (_font, 
					"Pareltje is stewardess en werkt bij het vliegmaatschappij Pidgairway. \n" +
					"Door haar werk vliegt Pareltje de hele wereld rond. Helaas verblijft \n" +
					"Pareltje niet lang in het land waar zij zich op dat moment bevindt. \n" +
					"Dit vind Pareltje erg jammer, want hierdoor kan zij niet veel van het \n" +
					"land zien. Elke keer wanneer Pareltje het naar haar zin heeft vertrekt \n" +
					"zij weer naar de volgende bestemming.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Wat vind Pareltje jammer?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Dat ze niet veel van het land kan zien waar zij zich op dat moment bevindt", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Dat ze niet lang verblijft in het land waar zij zich op dat moment bevindt", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Dat ze niet veel van het land kan zien", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Dat ze vertrekt naar de volgende bestemming", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 9");
				break;
			case 10:

				spriteBatch.DrawString (_font, 
					"Pareltje werkt als stewardess en kan hierdoor soms dagen van huis \n" +
					"zijn. Als Pareltje soms drie dagen van huis is missen Knoopje en \n" +
					"Kraaltje hun moeder. Gelukkig is papa minder streng en mogen zij \n" +
					"langer op blijven, als zij de volgende dag vrij van school hebben.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom mogen Kraaltje en Knoopje soms langer opblijven?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Omdat Pareltje soms dagen van huis is", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Omdat papa minder streng is", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat zij dan de volgende dag vrij van school hebben", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Omdat Kraaltje en Knoopje hun moeder missen", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("VRAAG 10");
				break;
			}

			// BOSS LEVEL
			switch (Assets.BossGenerateNumber) {
			case 1:
				spriteBatch.DrawString (_font, 
					"Kraaltje en Knoopje zitten allebei op vliegles. Tijdens vliegles \n" +
					"leren zij hoe zij moeten vliegen als het hard waait, maar ook \n" +
					"hoe zij vanuit de lucht voedsel moeten zoeken. De vlieglesleraar \n" +
					"leert hun hoe zij dan moeten duiken.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Wat leren Kraaltje en Knoopje tijdens vliegles?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Hoe zij moeten duiken", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Hoe zij vanuit de lucht voedsel moeten zoeken", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Vliegles", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Hoe zij moeten duiken als zij vanuit de lucht voedsel hebben gezocht.", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("BOSS VRAAG 1");
				break;
			case 2:
				spriteBatch.DrawString (_font, 
					"Tijdens een vakantie naar Egypte waren Pidgey en Pareltje Knoopje \n" +
					"kwijt geraakt. Na een aantal rondje te hebben gevlogen op de plek \n" +
					"zij elkaar voor het laatst hadden gezien vonden zij elkaar weer. \n" +
					"Voortaan moest Knoopje altijd voor de anderen vliegen zodat zijn \n" +
					"ouders een oogje in het zeil konden houden.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Wie waren Pidgey en Pareltje kwijt geraakt tijdens een vakantie in Egypte?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Knoopje", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Kraaltje", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. De reisgids", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Pareltje", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("BOSS VRAAG 2");
				break;
			case 3:
				spriteBatch.DrawString (_font, 
					"De duivenfamilie heeft al jaren last van vossen die wel een \n" +
					"vogeltje lusten. Daarom heeft Pidgey zijn nest hoog in een \n" +
					"boom gebouw. De vossen kunnen erg gemeen zijn en het nest je \n" +
					"raken met steentjes die zij gooien. Doordat de nest hoog in \n" +
					"de boom zit kan de nest nooit geraakt worden.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom kunnen de vossen de nest van Pidgey nooit raken?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Omdat deze hoog in de boom zit", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Omdat de vossen erg gemeen zijn", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat de duivenfamilie last heeft van vossen", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Omdat de vossen niet goed kunnen gooien", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("BOSS VRAAG 3");
				break;
			case 4:
				spriteBatch.DrawString (_font, 
					"Oom Pidg is de broer van Pidgey. Pidg komt een aantal keer per \n" +
					"jaar op bezoek. Kraaltje en Knoopje kunnen niet wachten op de \n" +
					"Kerst is, want dan komt oom Pidg op bezoek. Oom Pidg is namelijk \n" +
					"een piraat die elke dag een avonduur meemaakt. Kraaltje en Knoopje \n" +
					"zijn dol op Pidg's verhalen die gaan over het leven op zee.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom kunnen Kraaltje en Knoopje niet wachten op de Kerst?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Omdat oom Pidg een piraat is", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Omdat oom Pidg maar een aantal keer per jaar op bezoek komt", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat oom Pidg elke dag een avonduur meemaakt", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Omdat Kraaltje en Knoopje dol zijn op verhalen van oom Pidg", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("BOSS VRAAG 4");
				break;
			case 5:
				spriteBatch.DrawString (_font, 
					"Kraaltje en Knoopje kunnen moeilijk slapen, omdat zij morgen op \n" +
					"schooleis gaan. Daarom heeft Pareltje een kommetje beddensoep \n" +
					"klaargezet. Beddensoep helpt tegen altijd tegen slapeloosheid. \n" +
					"Kraaltje en Knoopje hopen dat hierdoor de zenuwen wegtrekken en \n" +
					"zij weer verder kunnen slapen.", new Vector2 (50, 50), Color.White);

				spriteBatch.DrawString (_font, "Waarom heeft Pareltje een kommetje beddensoep klaar gezet?", new Vector2 (50, 400), Color.White);

				spriteBatch.DrawString (_font, "1. Omdat beddensoep helpt tegen slapeloosheid", new Vector2 (50, 450), Color.White);
				spriteBatch.DrawString (_font, "2. Omdat hierdoor de zenuwen wegtrekken", new Vector2 (50, 480), Color.White);
				spriteBatch.DrawString (_font, "3. Omdat Kraaltje en Knoopje moeilijk kunnen slapen", new Vector2 (50, 510), Color.White);
				spriteBatch.DrawString (_font, "4. Omdat Kraaltje en Knoopje morgen op schoolreis gaan", new Vector2 (50, 540), Color.White);
				Console.WriteLine ("BOSS VRAAG 5");
				break;
			}
		}
	}
}

