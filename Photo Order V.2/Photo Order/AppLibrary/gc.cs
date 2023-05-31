using System;

namespace AppLibrary
{
    public class GC {
        public static void GCCollect(
            int generation = 2, 
            GCCollectionMode mode = GCCollectionMode.Forced, 
            bool blocking = true, 
            bool compacting = true
        ) {
            System.GC.Collect(generation, mode, blocking, compacting);
        }
    }
}
