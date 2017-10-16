using Accord.Neuro;
using Accord.Neuro.Learning;
using InfluenceBot.GUI.Model;
using System.Linq;

namespace InfluenceBot.GUI.BusinessLogic
{
    public class ReinforceStateNN
    {
        public ActivationNetwork network;
        public ResilientBackpropagationLearning teacher;

        public ReinforceStateNN()
        {
            var activationFunction = new Accord.Neuro.ActivationFunctions.GaussianFunction();
            network = new ActivationNetwork(new SigmoidFunction(0.01), 4 + 4 + 4 * 5 * 5 + 25, new[] { 100, 100, 1 });
            new GaussianWeights(network, 0.1).Randomize();
            teacher = new ResilientBackpropagationLearning(network);
            teacher.LearningRate = 0.1;
        }

        public double Evaluate(ReinforceState attackState)
            => network.Compute(attackState.State).First();

        internal void Load(string path)
        {
            network = (ActivationNetwork)Network.Load(path);
            teacher = new ResilientBackpropagationLearning(network);
        }
    }
}
