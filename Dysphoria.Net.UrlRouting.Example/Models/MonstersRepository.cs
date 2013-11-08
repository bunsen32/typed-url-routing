namespace Dysphoria.Net.UrlRouting.Test.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class MonstersRepository : IDisposable
	{
		private static readonly MonstersRepository Instance = new MonstersRepository();
		public ICollection<Monster> Monsters { get; private set; }
		public ICollection<string> Categories { get; private set; }

		private MonstersRepository()
		{
			var monsters = new List<Monster>();
			monsters.AddRange(InitialMonsters());
			this.Monsters = monsters;

			var categories = new List<string>();
			categories.AddRange(monsters.SelectMany(m => m.Categories).Distinct());
			this.Categories = categories;

			this.SaveChanges();
		}

		public static MonstersRepository Get()
		{
			return Instance;
		}

		public void SaveChanges()
		{
			// Simulate a real database and assign any unassigned 'id' fields.
			foreach (var monster in this.Monsters)
			{
				// If no id, assign one:
				if (monster.Id == 0)
				{
					var maxIdOrNull = this.Monsters.Max(m => (int?)m.Id);
					monster.Id = (maxIdOrNull ?? 0) + 1;
				}
			}

			// Also remove any superceded records.
			var byId = this.Monsters
				.GroupBy(m => m.Id)
				.Where(g => g.Count() > 1);
			foreach (var group in byId)
			{
				var allButLast = group.Reverse().Skip(1);
				foreach (var obsolete in allButLast)
				{
					this.Monsters.Remove(obsolete);
				}
			}
		}

		public void Dispose()
		{
			// Do nothing. This is just so we can use it in a 'using' statement 
			// like you might do with a real database.
		}

		private IEnumerable<Monster> InitialMonsters()
		{
			return new Monster[]{
				new Monster()
				{
					Name = "Sasquatch",
					Description = "Hides in the snow, and has enormous feet. Leaves Big Footprints",
					Categories = new string[]{"shaggy"},
				},
				new Monster()
				{
					Name = "Zombie",
					Description = "One of the dead which has risen from the grave to feast on the brains " +
					"of the living. Shambles. Limited vocabulary. To kill, remove the head or destroy the brain.",
					Categories = new string[] { "undead" },
					IsDangerous = true,
				},
				new Monster(){
					Name = "The Mummy",
					Description = "Animated by an ancient curse, this preserved Egyptian king must " +
					"take revenge on those who desecrated its tomb.",
					Categories = new string[]{"undead", "cursed"},
					IsDangerous = true,
				},
				new Monster(){
					Name = "Warewolf",
					Description = "By the light of the silvery moon... this cursed human transforms into " +
					"a baying lycanthrope, half-man-half-wolf. Can only be injured by silver bullets.",
					Categories = new string[]{"shaggy", "cursed"},
					IsDangerous = true,
				},
				new Monster(){
					Name = "Vampyre",
					Description = "Ancient Transylvanian nobles who are overly bloodthirsty in life tend to " +
					"rise as vampires in death. They sleep by day and seduce and kill by night. Can only be " +
					"permanently killed by driving a steak through their black hearts. Scared of crosses, " +
					"averse to garlic, and drawn to women in nighties.",
					Categories = new string[]{"undead"},
					IsDangerous = true,
				},
				new Monster(){
					Name = "Nessie",
					Description = "A lonely old Plesiosaur stranded in Loch Ness",
 					Categories = new string[]{"scottish"},
					IsDangerous = true,
				},
			};
		}
	}
}